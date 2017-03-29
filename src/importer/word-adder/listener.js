const Promise = require('bluebird-tools');

const config = require('./config');
const helpers = require('./helpers');

function listener() {
	return msg => {
		msg.touch();
		const word = msg.json();

		config.logger.info('word to add received:', word);

		Promise.monitor('find word', () => helpers.findWord(word))
			.whenLog('info', w => w, 'word already exists, quiting')
			.unlessLog('info', w => w, 'word not exists, saving word')
			.unlessMonitor('save word', w => w, () => helpers.saveWord({_id: word}))
			.info('finishing')
			.then(() => msg.finish())
			.catch(err => {
				config.logger.error(err);
				msg.requeue(15 * 60 * 1000);
			});
	};
}

module.exports = listener;

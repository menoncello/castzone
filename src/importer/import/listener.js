const Promise = require('bluebird-tools');

const config = require('./config');
const helpers = require('./helpers');

function listener() {
	return msg => {
		msg.touch();

		const word = msg.json();
		config.logger.info('Word to search:', word);

		Promise.monitor('search iTunes', helpers.searchPodcast(word._id))
			.whenLog('info', data => data.length, 'no podcast to import')
			.unless(data => data.length, data => {
				Promise.resolve()
					.thenMonitor('get existing podcasts', () => helpers.existingPodcasts(data))
					.thenMonitor('send to import', () => config.tools.importPub.publish(data));
			})
			.info('finishing')
			.then(msg.finish)
			.catch(err => {
				config.logger.error(err);
				msg.requeue(15 * 60 * 1000);
			});
	};
}

module.exports = listener;

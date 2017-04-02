const Promise = require('bluebird-tools');
const now = require('performance-now');

const config = require('./config');
const helpers = require('./helpers');

config.logger.info('Starting words scheduler');

// check if is test is running
if (process.argv[1].indexOf('/_mocha') === -1) {
	run();
}

function run() {
	const start = now();

	return Promise.monitor('load words', helpers.getAllWords)
		.whenLog('debug', helpers.wordLength, 'scheduling the words')
		.unlessLog('debug', helpers.wordLength, 'importing the default words')
		.iif(helpers.wordLength, helpers.publishWords, helpers.publishDefaultWords)
		.catch(helpers.error)
		.finally(() => config.logger.info('finishing in', (now() - start).toFixed(3), 'ms'));
}

module.exports = run;
const Promise = require('bluebird-tools');

const config = require('./config');
const helpers = require('./helpers');
const listener = require('./listener');

config.logger.info('Starting word adder');

Promise.monitor('create word topic', helpers.createWordTopic)
	.thenMonitor('create add word topic', helpers.createAddWordTopic)
	.thenMonitor('create import topic', helpers.createImportTopic)
	.thenMonitor('create import podcast channel', helpers.createImportPodcastChannel)
	.info('start listening messages')
	.then(() => config.tools.listener())
	.catch(config.logger.error);

config.tools.listener.on('message', listener);

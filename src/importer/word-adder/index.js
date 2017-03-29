const config = require('./config');
const listener = require('./listener');
const helpers = require('./helpers');

config.logger.info('Starting word adder');

Promise.monitor('create word topic', helpers.createWordTopic)
	.thenMonitor('create add-word topic', helpers.createAddWordTopic)
	.thenMonitor('create save-word channel', helpers.createAddWordChannel)
	.info('Listening messages')
	.then(() => config.tools.listener())
	.catch(helpers.error);

config.tools.listener.on('message', listener);

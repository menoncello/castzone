const config = require('./config');
const listener = require('./listener');
const helpers = require('./helpers');

config.logger.info('Starting word adder');

Promise.monitor('listening', () => config.tools.listener())
	.catch(helpers.error);

config.tools.listener.on('message', listener);

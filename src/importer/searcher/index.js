const Promise = require('bluebird-tools');

const config = require('./config');
const listener = require('./listener');

config.logger.info('Starting word adder');

Promise.monitor(() => config.tools.listener.listen())
	.catch(config.logger.error);

config.tools.listener.on('message', listener);

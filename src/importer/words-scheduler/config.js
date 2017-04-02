const Publisher = require('nsq-publisher');
const winston = require('winston');

const logger = new (winston.Logger)({
	level: 'debug',
	transports: [
		new (winston.transports.Console)({
			colorize: 'all',
		}),
		new (winston.transports.File)({
			filename: 'importer.words-scheduler.log',
			maxsize: 1024 * 1024 * 10,
			maxFiles: 10,
			zippedArchive: true,
			prettyPrint: true,
		}),
	],
});

module.exports.nsq = {
	dataTcpPort: () => parseInt(process.env.NSQ_DATA_TCP_PORT || 4150, 10),
	dataHttpPort: () => parseInt(process.env.NSQ_DATA_HTTP_PORT || 4151, 10),
	dataUrl: () => process.env.NSQ_DATA_URL || '192.168.99.100',
	wordTopic: () => process.env.NSQ_WORD_TOPIC || 'cz-words',
	wordAddTopic: () => process.env.NSQ_WORD_TOPIC || 'cz-word-add',
};

module.exports.mongo = {
	url: () => process.env.MONGO_URL || 'mongodb://192.168.99.100:27017/cz-importer',
	words: () => process.env.MONGO_WORDS || 'words',
};

module.exports.options = {
	defaultWords: () => [
		'nerd',
		'anticast',
		'f1',
		'race',
		'test',
		'apple',
		'iradex',
		'atheism',
	],
};

module.exports.tools = {
	wordPub: new Publisher({
		dataUrl: module.exports.nsq.dataUrl(),
		dataHttpPort: module.exports.nsq.dataHttpPort(),
		dataTcpPort: module.exports.nsq.dataTcpPort(),
		topic: module.exports.nsq.wordTopic(),
		autoCreate: true,
	}),
	addPub: new Publisher({
		dataUrl: module.exports.nsq.dataUrl(),
		dataHttpPort: module.exports.nsq.dataHttpPort(),
		dataTcpPort: module.exports.nsq.dataTcpPort(),
		topic: module.exports.nsq.wordAddTopic(),
		autoCreate: true,
	}),
};

module.exports.logger = logger;

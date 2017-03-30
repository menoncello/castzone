const Listener = require('nsq-listener');
const Publisher = require('nsq-publisher');
const winston = require('winston');

const logger = new (winston.Logger)({
	level: 'debug',
	transports: [
		new (winston.transports.Console)({
			colorize: 'all',
		}),
		new (winston.transports.File)({
			filename: 'importer.searcher.log',
			maxsize: 1024 * 1024 * 10,
			maxFiles: 10,
			zippedArchive: true,
			prettyPrint: true,
		}),
	],
});

module.exports.nsq = {
	lookupPort: () => parseInt(process.env.NSQ_LOOKUP_PORT || 4161, 10),
	lookupUrl: () => process.env.NSQ_LOOKUP_URL || '192.168.99.100',
	dataTcpPort: () => parseInt(process.env.NSQ_DATA_TCP_PORT || 4150, 10),
	dataHttpPort: () => parseInt(process.env.NSQ_DATA_HTTP_PORT || 4151, 10),
	dataUrl: () => process.env.NSQ_DATA_URL || '192.168.99.100',
	wordToAddTopic: () => process.env.NSQ_ADD_WORD_TOPIC || 'cz-word-add',
	wordTopic: () => process.env.NSQ_WORD_TOPIC || 'cz-words',
	importTopic: () => process.env.NSQ_IMPORT_TOPIC || 'cz-import',
	importPodcastTopic: () => process.env.NSQ_IMPORT_PODCAST_TOPIC || 'cz-import-podcast',
};

module.exports.options = {
	reimportDays: () => parseInt(process.env.REIMPORT_DAYS || 5, 10),
};

module.exports.mongo = {
	url: () => process.env.MONGO_URL || 'mongodb://192.168.99.100:27017/cz-importer',
	words: () => process.env.MONGO_WORDS || 'words',
	// podcasts: () => process.env.MONGO_PODCASTS || 'podcasts-imported',
};

module.exports.tools = {
	listener: new Listener({
		dataUrl: module.exports.nsq.dataUrl(),
		dataUrlPort: module.exports.nsq.dataHttpPort(),
		dataTcpPort: module.exports.nsq.dataTcpPort(),
		lookupUrl: module.exports.nsq.lookupUrl(),
		lookupPort: module.exports.nsq.lookupPort(),
		topic: module.exports.nsq.wordTopic(),
		channel: module.exports.nsq.importPodcastTopic(),
	}),
	addWordPub: new Publisher({
		dataUrl: module.exports.nsq.dataUrl(),
		dataHttpPort: module.exports.nsq.dataHttpPort(),
		dataTcpPort: module.exports.nsq.dataTcpPort(),
		topic: module.exports.nsq.wordToAddTopic(),
	}),
	importPub: new Publisher({
		dataUrl: module.exports.nsq.dataUrl(),
		dataHttpPort: module.exports.nsq.dataHttpPort(),
		dataTcpPort: module.exports.nsq.dataTcpPort(),
		topic: module.exports.nsq.importTopic(),
	}),
};

module.exports.logger = logger;

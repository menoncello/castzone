const MongoClient = require('mongodb').MongoClient;
const Promise = require('bluebird-tools');

const config = require('./config');

module.exports.createWordTopic = () => config.tools.wordPub.createTopic();

module.exports.createAddWordTopic = () => config.tools.addPub.createTopic();

module.exports.createAddWordChannel = () => config.tools.listener.createChannel();

module.exports.findWord = (word) => Promise.convert(MongoClient.connect(config.mongo.url()))
	.then(db => Promise.convert(db.collection(config.mongo.words())
		.findOne({_id: word}))
		.then(() => db.close()));

module.exports.saveWord = (word) => Promise.convert(MongoClient.connect(config.mongo.url()))
	.then(db => Promise.convert(db.collection(config.mongo.words())
		.insertOne({_id: word}))
		.then(() => db.close()));

module.exports.wordLength = w => w.length;

module.exports.publishWords = w => config.tools.wordPub.publish(w);

module.exports.error = err => config.logger.error(err);

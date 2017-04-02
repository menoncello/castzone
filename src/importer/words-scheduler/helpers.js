const MongoClient = require('mongodb').MongoClient;
const config = require('./config');
const Promise = require('bluebird-tools');

module.exports.getAllWords = () => Promise.convert(MongoClient.connect(config.mongo.url()))
	.then(db => Promise.convert(db.collection(config.mongo.words()).find({}).toArray()).then(() => db.close()));

module.exports.wordLength = w => w.length;

module.exports.publishWords = w => config.tools.wordPub.publish(w);

module.exports.publishDefaultWords = () => config.tools.addPub.publish(config.options.defaultWords());

module.exports.error = err => config.logger.error(err);

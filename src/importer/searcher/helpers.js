const MongoClient = require('mongodb').MongoClient;
const Promise = require('bluebird-tools');
const iTunes = require('itunes-api-search');

const config = require('./config');

module.exports.searchPodcast = (word) => iTunes.search(word, { entity: 'podcast', limit: 200 });

module.exports.publishWords = w => config.tools.wordPub.publish(w);

module.exports.error = err => config.logger.error(err);

module.exports.existingPodcasts = podcasts => Promise.convert(MongoClient.connect(config.mongo.url()))
	.then(db => Promise.convert(db.collection(config.mongo.importedPodcast())
		.find({_id: { $in: podcasts.map(x => x._id) }, lastUpdate: { $gt: config.options.reimportDays() } }).toArray())
		.then(() => db.close()));

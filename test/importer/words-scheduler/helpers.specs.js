const Promise = require('bluebird-tools');
const assert = require('chai').assert;
const mockery = require('mockery');
const sinon = require('sinon');

describe('word-scheduler - helpers', () => {
	let configMock;
	let mongodbMock;
	let helpers;
	let collection;
	let find;
	const defaultWords = ['a', 'b', 'c'];

	before(() => {
		mockery.enable({
			warnOnReplace: false,
			warnOnUnregistered: false,
			useCleanCache: true,
		});
		configMock = {
			tools: {
				wordPub: {
					createTopic: sinon.stub().returns(Promise.resolve()),
					publish: sinon.stub().returns(Promise.resolve()),
				},
				addPub: {
					createTopic: sinon.stub().returns(Promise.resolve()),
					publish: sinon.stub().withArgs(defaultWords).returns(Promise.resolve()),
				},
			},
			mongo: {
				url: () => 'mongodb://mongotest:27017/test',
				words: () => 'words',
			},
			options: {
				defaultWords: () => defaultWords,
			},
			logger: {
				error: sinon.stub(),
			},
		};
		find = { toArray: sinon.stub().returns(Promise.resolve()) };
		collection = { find: () => find };
		mongodbMock = {
			MongoClient: {
				connect: sinon.stub().returns(Promise.resolve({
					collection: () => collection,
					close: () => {},
				})),
			},
		};

		mockery.registerMock('./config', configMock);
		mockery.registerMock('mongodb', mongodbMock);

		helpers = require('../../../src/importer/words-scheduler/helpers');
	});

	after(() => mockery.disable());

	context('#createWordTopic', () => {
		it('should call wordPub.createTopic once', () => {
			helpers.createWordTopic();

			assert.isTrue(configMock.tools.wordPub.createTopic.calledOnce);
		});
		it('should returns a not null object', () => {
			const promise = helpers.createWordTopic();

			assert.isNotNull(promise);
		});
		it('should returns a Bluebird Promise', () => {
			const promise = helpers.createWordTopic();

			assert.isTrue(promise.isBluebird);
		});
	});
	context('#createAddWordTopic', () => {
		it('should call wordAddPub.createTopic once', () => {
			helpers.createAddWordTopic();

			assert.isTrue(configMock.tools.addPub.createTopic.calledOnce);
		});
		it('should returns a not null object', () => {
			const promise = helpers.createAddWordTopic();

			assert.isNotNull(promise);
		});
		it('should returns a Bluebird Promise', () => {
			const promise = helpers.createAddWordTopic();

			assert.isTrue(promise.isBluebird);
		});
	});
	context('#getAllWords', () => {
		it('should returns a not null object', () => {
			const promise = helpers.getAllWords();

			assert.isNotNull(promise);
		});
		it('should returns a Bluebird Promise', () => {
			const promise = helpers.getAllWords();

			assert.isTrue(promise.isBluebird);
		});
	});
	context('#wordLength', () => {
		it('should returns the length of the words', () => {
			const result = helpers.wordLength(['', '', '']);
			assert.equal(3, result);
		});
	});
	context('#publishWords', () => {
		it('should call wordPub.publish once', () => {
			helpers.publishWords();

			assert.isTrue(configMock.tools.wordPub.publish.calledOnce);
		});
		it('should returns a not null object', () => {
			const promise = helpers.publishWords();

			assert.isNotNull(promise);
		});
		it('should returns a Bluebird Promise', () => {
			const promise = helpers.publishWords();

			assert.isTrue(promise.isBluebird);
		});
		it('should be passed throw what has received', () => {
			const array = ['', '', '', ''];
			helpers.publishWords(array);
			assert.isTrue(configMock.tools.wordPub.publish.calledWith(array));
		});
	});
	context('#publishDefaultWords', () => {
		it('should call addPub.publish once', () => {
			helpers.publishDefaultWords();

			assert.isTrue(configMock.tools.addPub.publish.calledOnce);
		});
		it('should returns a not null object', () => {
			const promise = helpers.publishDefaultWords();

			assert.isNotNull(promise);
		});
		it('should returns a Bluebird Promise', () => {
			const promise = helpers.publishDefaultWords();

			assert.isTrue(promise.isBluebird);
		});
		it('should be passed throw what has received', () => {
			helpers.publishDefaultWords();
			assert.isTrue(configMock.tools.addPub.publish.calledWith(defaultWords));
		});
	});
	context('#error', () => {
		it('should calls logger.error once', () => {
			const err = 3;
			helpers.error(err);
			assert.isTrue(configMock.logger.error.calledOnce);
		});
	});
});

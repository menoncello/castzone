const Promise = require('bluebird-tools');
const assert = require('chai').assert;
const mockery = require('mockery');
const sinon = require('sinon');

suite('importer/word-scheduler/helpers', () => {
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

	suite('#getAllWords', () => {
		test('should returns a not null object', () => {
			const promise = helpers.getAllWords();

			assert.isNotNull(promise);
		});
		test('should returns a Bluebird Promise', () => {
			const promise = helpers.getAllWords();

			assert.isTrue(promise.isBluebird);
		});
	});
	suite('#wordLength', () => {
		test('should returns the length of the words', () => {
			const result = helpers.wordLength(['', '', '']);
			assert.equal(3, result);
		});
	});
	suite('#publishWords', () => {
		test('should call wordPub.publish once', () => {
			helpers.publishWords();

			assert.isTrue(configMock.tools.wordPub.publish.calledOnce);
		});
		test('should returns a not null object', () => {
			const promise = helpers.publishWords();

			assert.isNotNull(promise);
		});
		test('should returns a Bluebird Promise', () => {
			const promise = helpers.publishWords();

			assert.isTrue(promise.isBluebird);
		});
		test('should be passed throw what has received', () => {
			const array = ['', '', '', ''];
			helpers.publishWords(array);
			assert.isTrue(configMock.tools.wordPub.publish.calledWith(array));
		});
	});
	suite('#publishDefaultWords', () => {
		test('should call addPub.publish once', () => {
			helpers.publishDefaultWords();

			assert.isTrue(configMock.tools.addPub.publish.calledOnce);
		});
		test('should returns a not null object', () => {
			const promise = helpers.publishDefaultWords();

			assert.isNotNull(promise);
		});
		test('should returns a Bluebird Promise', () => {
			const promise = helpers.publishDefaultWords();

			assert.isTrue(promise.isBluebird);
		});
		test('should be passed throw what has received', () => {
			helpers.publishDefaultWords();
			assert.isTrue(configMock.tools.addPub.publish.calledWith(defaultWords));
		});
	});
	suite('#error', () => {
		test('should calls logger.error once', () => {
			const err = 3;
			helpers.error(err);
			assert.isTrue(configMock.logger.error.calledOnce);
		});
	});
});

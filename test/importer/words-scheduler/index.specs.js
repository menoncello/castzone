const Promise = require('bluebird-tools');
const expect = require('chai').expect;
const mockery = require('mockery');
const sinon = require('sinon');

suite('importer/word-scheduler/index', () => {
	const sandbox = sinon.sandbox.create();
	const configMock = {
		logger: {
			info: () => {},
			log: () => {},
		},
	};
	const helpersMock = {
		getAllWords: () => {},
		wordLength: () => {},
		publishWords: () => {},
		publishDefaultWords: () => {},
		error: () => {},
	};
	let index;

	before(() => {
		mockery.enable({
			warnOnReplace: false,
			warnOnUnregistered: false,
			useCleanCache: true,
		});

		Promise.configureLog((level, text, ...args) => configMock.logger.log(level, text, ...args));

		mockery.registerMock('./config', configMock);
		mockery.registerMock('./helpers', helpersMock);
	});

	after(() => mockery.disable());

	beforeEach(() => {
		sandbox.stub(helpersMock, 'getAllWords').returns(Promise.resolve());
		sandbox.stub(helpersMock, 'wordLength').returns(0);
		sandbox.stub(helpersMock, 'publishWords').returns(Promise.resolve());
		sandbox.stub(helpersMock, 'publishDefaultWords').returns(Promise.resolve());
		sandbox.stub(helpersMock, 'error').returns(Promise.resolve());

		sandbox.stub(configMock.logger, 'info').returns(Promise.resolve());
		sandbox.stub(configMock.logger, 'log').returns(Promise.resolve());

		index = require('../../../src/importer/words-scheduler/index');
	});

	afterEach(() => sandbox.restore());

	suite('#run', () => {
		test('should run getAllWords once', (done) => {
			index()
				.finally(() => {
					expect(helpersMock.getAllWords.calledOnce).to.be.true;
					done();
				});
		});
		test('with no returns from wordLength, should calls publishDefaultWords', (done) => {
			index()
				.finally(() => {
					expect(helpersMock.publishDefaultWords.calledOnce).to.be.true;
					done();
				});
		});
		test('with 10 wordLength, should calls publishWords', (done) => {
			helpersMock.wordLength.returns(10);
			index()
				.finally(() => {
					expect(helpersMock.publishWords.calledOnce).to.be.true;
					done();
				});
		});
		test('with 1 word, should pass to publishWords that word as list', (done) => {
			const words = [{ _id: 'word' }];

			helpersMock.wordLength.returns(1);
			helpersMock.getAllWords.returns(Promise.resolve(words));

			index()
				.finally(() => {
					expect(helpersMock.publishWords.args[0][0]).to.deep.equal(words);
					done();
				});
		});
	});
});

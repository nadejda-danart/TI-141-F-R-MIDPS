'use strict';

let Loki = require('lokijs');

module.exports = class DBManager {
  constructor(callback) {
    this.postStartCallback = callback;
    this.db = new Loki(DBManager.DB_PATH ,
      {
        autosave : true,
        autoload: true,
        autoloadCallback: this.dbInitialized.bind(this)
      }
    );

  }

  /**
   * Post database initialization callback
   */
  dbInitialized() {
    this.table = this.db.getCollection('questions') || this.db.addCollection('questions');

    this.postStartCallback();
  }

  /**
   *
   * @param {Object} question
   */
  addQuestion(question) {
    this.table.insert(question);
  }

  /**
   * Start db instance
   * @param {Function} callback
   * @returns {DBManager}
   */
  static dbStart(callback) {
    return new DBManager(callback);
  }

  /**
   * @returns {String}
   */
  static get DB_PATH() {
    return './database/db.json';
  }
};

'use strict';

let fs = require('fs');
let path = require('path');

class ImageFetcher {
  constructor(path) {
    this.path = path;
  }

  getAll() {
    return fs.readdirSync(ImageFetcher.PORTFOLIO_FODLER).map((el) => {
      return {
        path: `${this.path}/${el}`,
        name: el.replace(/(.*)\.[^.]+$/, '$1')
      }
    });
  }

  getLast9() {
    let allImages = fs.readdirSync(ImageFetcher.PORTFOLIO_FODLER).map(el => {
      let obj = {};
      obj[el] = fs.statSync(path.join(ImageFetcher.PORTFOLIO_FODLER, el));

      return obj;
    });

    return allImages.sort((a, b) => {
      if (a.ctime < b.ctime) return 1;
      if (a.ctime > b.ctime) return -1;
      return 0;
    }).slice(0, 9).map((el) => {
      let key = Object.keys(el)[0];
      return {
        path: `${this.path}/${key}`,
        name: key.replace(/(.*)\.[^.]+$/, '$1')
      }
    });
  }

  static get PORTFOLIO_FODLER() {
    return './portofoliu';
  }
}

module.exports = ImageFetcher;
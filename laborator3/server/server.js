'use strict';

const portfolioVirtualPath = 'works';

let ImageFetcher = require('./ImageFetcher');
let DBFetcher = require('./DatabaseFetcher');
let express = require('express');
let bodyParser = require('body-parser');

let app = express();
let imageFetcher = new ImageFetcher(portfolioVirtualPath);

app.use(express.static('../website'));
app.use('/portofoliu', express.static('../website/portofoliu.html'));
app.use(`/${portfolioVirtualPath}`, express.static('./portofoliu'));
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({ extended: true}));

app.get('/portofolio', (req, res) => {
  res.send(imageFetcher.getAll());
});

app.get('/portofolio-preview', (req, res) => {
  res.send(imageFetcher.getLast9());
});

app.post('/question', (req, res) => {
  databaseManager.addQuestion(req.body);

  res.send({});
});


let databaseManager = DBFetcher.dbStart(() => {
  app.listen(7331);
});

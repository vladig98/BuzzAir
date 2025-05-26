/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.cs_CZ = factory()));
}(this, function () { 'use strict';

  // Czech
  var translation = {
    firstDayOfWeek: 1,
    template: 'D. MMMM YYYY',
    templateHookWords: {
      MMM: ['led', 'úno', 'bře', 'dub', 'kvě', 'čer', 'čvc', 'srp', 'zář', 'říj', 'lis', 'pro'],
      MMMM: ['leden', 'únor', 'březen', 'duben', 'květen', 'červen', 'červenec', 'srpen', 'září', 'říjen', 'listopad', 'prosinec'],
      DDD: ['ne', 'po', 'út', 'st', 'čt', 'pá', 'so'],
      DDDD: ['neděle', 'pondělí', 'úterý', 'středa', 'čtvrtek', 'pátek', 'sobota']
    }
  };

  return translation;

}));

/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.bs_BA = factory()));
}(this, function () { 'use strict';

  // Bosnian
  var translation = {
    firstDayOfWeek: 1,
    template: 'DD. MMMM YYYY.',
    templateHookWords: {
      MMM: ['jan', 'feb', 'mar', 'apr', 'maj', 'jun', 'jul', 'aug', 'sep', 'okt', 'nov', 'dec'],
      MMMM: ['januar', 'februar', 'mart', 'april', 'maj', 'juni', 'juli', 'august', 'septembar', 'oktobar', 'novembar', 'decembar'],
      DDD: ['ne', 'po', 'ut', 'sr', 'če', 'pe', 'su'],
      DDDD: ['nedjelja', 'ponedjeljak', 'utorak', 'srijeda', 'cetvrtak', 'petak', 'subota']
    }
  };

  return translation;

}));

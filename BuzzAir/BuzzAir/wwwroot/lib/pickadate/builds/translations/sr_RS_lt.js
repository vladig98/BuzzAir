/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.sr_RS_lt = factory()));
}(this, function () { 'use strict';

  // Serbian (Latin)
  var translation = {
    firstDayOfWeek: 1,
    template: 'D. MMMM YYYY.',
    templateHookWords: {
      MMM: ['jan.', 'feb.', 'mart', 'apr.', 'maj', 'jun', 'jul', 'avg.', 'sept.', 'okt.', 'nov.', 'dec.'],
      MMMM: ['januar', 'februar', 'mart', 'april', 'maj', 'jun', 'juli', 'avgust', 'septembar', 'oktobar', 'novembar', 'decembar'],
      DDD: ['ned.', 'pon.', 'ut.', 'sr.', 'čet.', 'pet.', 'sub.'],
      DDDD: ['nedelja', 'ponedeljak', 'utorak', 'sreda', 'četvrtak', 'petak', 'subota']
    }
  };

  return translation;

}));

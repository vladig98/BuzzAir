/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.sl_SI = factory()));
}(this, function () { 'use strict';

  // Slovenian
  var translation = {
    firstDayOfWeek: 1,
    template: 'D. MMMM YYYY',
    templateHookWords: {
      MMM: ['jan', 'feb', 'mar', 'apr', 'maj', 'jun', 'jul', 'avg', 'sep', 'okt', 'nov', 'dec'],
      MMMM: ['januar', 'februar', 'marec', 'april', 'maj', 'junij', 'julij', 'avgust', 'september', 'oktober', 'november', 'december'],
      DDD: ['ned', 'pon', 'tor', 'sre', 'čet', 'pet', 'sob'],
      DDDD: ['nedelja', 'ponedeljek', 'torek', 'sreda', 'četrtek', 'petek', 'sobota']
    }
  };

  return translation;

}));

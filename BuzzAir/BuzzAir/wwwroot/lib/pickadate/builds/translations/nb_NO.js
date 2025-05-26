/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.nb_NO = factory()));
}(this, function () { 'use strict';

  // Norwegian
  var translation = {
    firstDayOfWeek: 1,
    template: 'DD. MMM. YYYY',
    templateHookWords: {
      MMM: ['jan', 'feb', 'mar', 'apr', 'mai', 'jun', 'jul', 'aug', 'sep', 'okt', 'nov', 'des'],
      MMMM: ['januar', 'februar', 'mars', 'april', 'mai', 'juni', 'juli', 'august', 'september', 'oktober', 'november', 'desember'],
      DDD: ['søn', 'man', 'tir', 'ons', 'tor', 'fre', 'lør'],
      DDDD: ['søndag', 'mandag', 'tirsdag', 'onsdag', 'torsdag', 'fredag', 'lørdag']
    }
  };

  return translation;

}));

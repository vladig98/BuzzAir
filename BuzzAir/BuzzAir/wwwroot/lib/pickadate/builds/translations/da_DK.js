/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.da_DK = factory()));
}(this, function () { 'use strict';

  // Danish
  var translation = {
    firstDayOfWeek: 1,
    template: 'D. MMMM YYYY',
    templateHookWords: {
      MMM: ['jan', 'feb', 'mar', 'apr', 'maj', 'jun', 'jul', 'aug', 'sep', 'okt', 'nov', 'dec'],
      MMMM: ['januar', 'februar', 'marts', 'april', 'maj', 'juni', 'juli', 'august', 'september', 'oktober', 'november', 'december'],
      DDD: ['søn', 'man', 'tir', 'ons', 'tor', 'fre', 'lør'],
      DDDD: ['søndag', 'mandag', 'tirsdag', 'onsdag', 'torsdag', 'fredag', 'lørdag']
    }
  };

  return translation;

}));

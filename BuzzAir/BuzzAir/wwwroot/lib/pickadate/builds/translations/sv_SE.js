/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.sv_SE = factory()));
}(this, function () { 'use strict';

  // Swedish
  var translation = {
    firstDayOfWeek: 1,
    template: 'YYYY-MM-DD',
    templateHookWords: {
      MMM: ['jan', 'feb', 'mar', 'apr', 'maj', 'jun', 'jul', 'aug', 'sep', 'okt', 'nov', 'dec'],
      MMMM: ['januari', 'februari', 'mars', 'april', 'maj', 'juni', 'juli', 'augusti', 'september', 'oktober', 'november', 'december'],
      DDD: ['sön', 'mån', 'tis', 'ons', 'tor', 'fre', 'lör'],
      DDDD: ['söndag', 'måndag', 'tisdag', 'onsdag', 'torsdag', 'fredag', 'lördag']
    }
  };

  return translation;

}));

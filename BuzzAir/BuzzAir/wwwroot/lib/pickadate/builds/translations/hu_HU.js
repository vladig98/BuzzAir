/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.hu_HU = factory()));
}(this, function () { 'use strict';

  // Hungarian
  var translation = {
    firstDayOfWeek: 1,
    template: 'YYYY. MMMM DD.',
    templateHookWords: {
      MMM: ['jan', 'febr', 'márc', 'ápr', 'máj', 'jún', 'júl', 'aug', 'szept', 'okt', 'nov', 'dec'],
      MMMM: ['január', 'február', 'március', 'április', 'május', 'június', 'július', 'augusztus', 'szeptember', 'október', 'november', 'december'],
      DDD: ['V', 'H', 'K', 'Sze', 'CS', 'P', 'Szo'],
      DDDD: ['vasárnap', 'hétfő', 'kedd', 'szerda', 'csütörtök', 'péntek', 'szombat']
    }
  };

  return translation;

}));

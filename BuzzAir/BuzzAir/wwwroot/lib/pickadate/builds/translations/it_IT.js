/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.it_IT = factory()));
}(this, function () { 'use strict';

  // Italian
  var translation = {
    firstDayOfWeek: 1,
    template: 'DDDD D MMMM YYYY',
    templateHookWords: {
      MMM: ['gen', 'feb', 'mar', 'apr', 'mag', 'giu', 'lug', 'ago', 'set', 'ott', 'nov', 'dic'],
      MMMM: ['gennaio', 'febbraio', 'marzo', 'aprile', 'maggio', 'giugno', 'luglio', 'agosto', 'settembre', 'ottobre', 'novembre', 'dicembre'],
      DDD: ['dom', 'lun', 'mar', 'mer', 'gio', 'ven', 'sab'],
      DDDD: ['domenica', 'lunedì', 'martedì', 'mercoledì', 'giovedì', 'venerdì', 'sabato']
    }
  };

  return translation;

}));

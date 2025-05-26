/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.pl_PL = factory()));
}(this, function () { 'use strict';

  // Polish
  var translation = {
    firstDayOfWeek: 1,
    template: 'D MMMM YYYY',
    templateHookWords: {
      MMM: ['sty', 'lut', 'mar', 'kwi', 'maj', 'cze', 'lip', 'sie', 'wrz', 'paź', 'lis', 'gru'],
      MMMM: ['styczeń', 'luty', 'marzec', 'kwiecień', 'maj', 'czerwiec', 'lipiec', 'sierpień', 'wrzesień', 'październik', 'listopad', 'grudzień'],
      DDD: ['niedz.', 'pn.', 'wt.', 'śr.', 'cz.', 'pt.', 'sob.'],
      DDDD: ['niedziela', 'poniedziałek', 'wtorek', 'środa', 'czwartek', 'piątek', 'sobota']
    }
  };

  return translation;

}));

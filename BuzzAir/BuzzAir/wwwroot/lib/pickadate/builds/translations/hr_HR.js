/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.hr_HR = factory()));
}(this, function () { 'use strict';

  // Croatian
  var translation = {
    firstDayOfWeek: 1,
    template: 'D. MMMM YYYY.',
    templateHookWords: {
      MMM: ['sij', 'velj', 'ožu', 'tra', 'svi', 'lip', 'srp', 'kol', 'ruj', 'lis', 'stu', 'pro'],
      MMMM: ['siječanj', 'veljača', 'ožujak', 'travanj', 'svibanj', 'lipanj', 'srpanj', 'kolovoz', 'rujan', 'listopad', 'studeni', 'prosinac'],
      DDD: ['ned', 'pon', 'uto', 'sri', 'čet', 'pet', 'sub'],
      DDDD: ['nedjelja', 'ponedjeljak', 'utorak', 'srijeda', 'četvrtak', 'petak', 'subota']
    }
  };

  return translation;

}));

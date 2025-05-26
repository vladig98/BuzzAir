/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.gl_ES = factory()));
}(this, function () { 'use strict';

  // Galician
  var translation = {
    firstDayOfWeek: 1,
    template: 'DDDD D de MMMM de YYYY',
    templateHookWords: {
      MMM: ['xan', 'feb', 'mar', 'abr', 'mai', 'xun', 'xul', 'ago', 'sep', 'out', 'nov', 'dec'],
      MMMM: ['Xaneiro', 'Febreiro', 'Marzo', 'Abril', 'Maio', 'Xuño', 'Xullo', 'Agosto', 'Setembro', 'Outubro', 'Novembro', 'Decembro'],
      DDD: ['dom', 'lun', 'mar', 'mér', 'xov', 'ven', 'sab'],
      DDDD: ['domingo', 'luns', 'martes', 'mércores', 'xoves', 'venres', 'sábado']
    }
  };

  return translation;

}));

/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.pt_BR = factory()));
}(this, function () { 'use strict';

  // Brazilian Portuguese
  var translation = {
    firstDayOfWeek: 1,
    template: 'DDDD, D de MMMM de YYYY',
    templateHookWords: {
      MMM: ['jan', 'fev', 'mar', 'abr', 'mai', 'jun', 'jul', 'ago', 'set', 'out', 'nov', 'dez'],
      MMMM: ['janeiro', 'fevereiro', 'março', 'abril', 'maio', 'junho', 'julho', 'agosto', 'setembro', 'outubro', 'novembro', 'dezembro'],
      DDD: ['dom', 'seg', 'ter', 'qua', 'qui', 'sex', 'sab'],
      DDDD: ['domingo', 'segunda-feira', 'terça-feira', 'quarta-feira', 'quinta-feira', 'sexta-feira', 'sábado']
    }
  };

  return translation;

}));

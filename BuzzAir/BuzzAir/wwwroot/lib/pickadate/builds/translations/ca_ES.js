/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.ca_ES = factory()));
}(this, function () { 'use strict';

  // Catalan
  var translation = {
    firstDayOfWeek: 1,
    template: 'DDDD D de MMMM de YYYY',
    templateHookWords: {
      MMM: ['Gen', 'Feb', 'Mar', 'Abr', 'Mai', 'Jun', 'Jul', 'Ago', 'Set', 'Oct', 'Nov', 'Des'],
      MMMM: ['Gener', 'Febrer', 'Març', 'Abril', 'Maig', 'juny', 'Juliol', 'Agost', 'Setembre', 'Octubre', 'Novembre', 'Desembre'],
      DDD: ['diu', 'dil', 'dim', 'dmc', 'dij', 'div', 'dis'],
      DDDD: ['diumenge', 'dilluns', 'dimarts', 'dimecres', 'dijous', 'divendres', 'dissabte']
    }
  };

  return translation;

}));

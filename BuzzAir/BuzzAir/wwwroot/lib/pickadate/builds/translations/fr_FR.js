/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.fr_FR = factory()));
}(this, function () { 'use strict';

  // French
  var translation = {
    firstDayOfWeek: 1,
    template: 'DD MMMM YYYY',
    templateHookWords: {
      MMM: ['Jan', 'Fev', 'Mar', 'Avr', 'Mai', 'Juin', 'Juil', 'Aou', 'Sep', 'Oct', 'Nov', 'Dec'],
      MMMM: ['Janvier', 'Février', 'Mars', 'Avril', 'Mai', 'Juin', 'Juillet', 'Août', 'Septembre', 'Octobre', 'Novembre', 'Décembre'],
      DDD: ['Dim', 'Lun', 'Mar', 'Mer', 'Jeu', 'Ven', 'Sam'],
      DDDD: ['Dimanche', 'Lundi', 'Mardi', 'Mercredi', 'Jeudi', 'Vendredi', 'Samedi']
    }
  };

  return translation;

}));

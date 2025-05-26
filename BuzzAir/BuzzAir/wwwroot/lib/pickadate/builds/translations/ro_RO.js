/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.ro_RO = factory()));
}(this, function () { 'use strict';

  // Romanian
  var translation = {
    firstDayOfWeek: 1,
    template: 'DD MMMM YYYY',
    templateHookWords: {
      MMM: ['ian', 'feb', 'mar', 'apr', 'mai', 'iun', 'iul', 'aug', 'sep', 'oct', 'noi', 'dec'],
      MMMM: ['ianuarie', 'februarie', 'martie', 'aprilie', 'mai', 'iunie', 'iulie', 'august', 'septembrie', 'octombrie', 'noiembrie', 'decembrie'],
      DDD: ['D', 'L', 'Ma', 'Mi', 'J', 'V', 'S'],
      DDDD: ['duminică', 'luni', 'marţi', 'miercuri', 'joi', 'vineri', 'sâmbătă']
    }
  };

  return translation;

}));

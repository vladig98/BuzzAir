/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, (global.pickadate = global.pickadate || {}, global.pickadate.translations = global.pickadate.translations || {}, global.pickadate.translations.id_ID = factory()));
}(this, function () { 'use strict';

  // Indonesian
  var translation = {
    firstDayOfWeek: 1,
    template: 'D MMMM YYYY',
    templateHookWords: {
      MMM: ['Jan', 'Feb', 'Mar', 'Apr', 'Mei', 'Jun', 'Jul', 'Agu', 'Sep', 'Okt', 'Nov', 'Des'],
      MMMM: ['Januari', 'Februari', 'Maret', 'April', 'Mei', 'Juni', 'Juli', 'Agustus', 'September', 'Oktober', 'November', 'Desember'],
      DDD: ['Min', 'Sen', 'Sel', 'Rab', 'Kam', 'Jum', 'Sab'],
      DDDD: ['Minggu', 'Senin', 'Selasa', 'Rabu', 'Kamis', 'Jumat', 'Sabtu']
    }
  };

  return translation;

}));

/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory(require('react')) :
  typeof define === 'function' && define.amd ? define(['react'], factory) :
  (global = global || self, global.pickadate = factory(global.React));
}(this, function (React) { 'use strict';

  function _classCallCheck(instance, Constructor) {
    if (!(instance instanceof Constructor)) {
      throw new TypeError("Cannot call a class as a function");
    }
  }

  function _defineProperties(target, props) {
    for (var i = 0; i < props.length; i++) {
      var descriptor = props[i];
      descriptor.enumerable = descriptor.enumerable || false;
      descriptor.configurable = true;
      if ("value" in descriptor) descriptor.writable = true;
      Object.defineProperty(target, descriptor.key, descriptor);
    }
  }

  function _createClass(Constructor, protoProps, staticProps) {
    if (protoProps) _defineProperties(Constructor.prototype, protoProps);
    if (staticProps) _defineProperties(Constructor, staticProps);
    return Constructor;
  }

  function _defineProperty(obj, key, value) {
    if (key in obj) {
      Object.defineProperty(obj, key, {
        value: value,
        enumerable: true,
        configurable: true,
        writable: true
      });
    } else {
      obj[key] = value;
    }

    return obj;
  }

  function _extends() {
    _extends = Object.assign || function (target) {
      for (var i = 1; i < arguments.length; i++) {
        var source = arguments[i];

        for (var key in source) {
          if (Object.prototype.hasOwnProperty.call(source, key)) {
            target[key] = source[key];
          }
        }
      }

      return target;
    };

    return _extends.apply(this, arguments);
  }

  function _objectSpread(target) {
    for (var i = 1; i < arguments.length; i++) {
      var source = arguments[i] != null ? arguments[i] : {};
      var ownKeys = Object.keys(source);

      if (typeof Object.getOwnPropertySymbols === 'function') {
        ownKeys = ownKeys.concat(Object.getOwnPropertySymbols(source).filter(function (sym) {
          return Object.getOwnPropertyDescriptor(source, sym).enumerable;
        }));
      }

      ownKeys.forEach(function (key) {
        _defineProperty(target, key, source[key]);
      });
    }

    return target;
  }

  function _inherits(subClass, superClass) {
    if (typeof superClass !== "function" && superClass !== null) {
      throw new TypeError("Super expression must either be null or a function");
    }

    subClass.prototype = Object.create(superClass && superClass.prototype, {
      constructor: {
        value: subClass,
        writable: true,
        configurable: true
      }
    });
    if (superClass) _setPrototypeOf(subClass, superClass);
  }

  function _getPrototypeOf(o) {
    _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf : function _getPrototypeOf(o) {
      return o.__proto__ || Object.getPrototypeOf(o);
    };
    return _getPrototypeOf(o);
  }

  function _setPrototypeOf(o, p) {
    _setPrototypeOf = Object.setPrototypeOf || function _setPrototypeOf(o, p) {
      o.__proto__ = p;
      return o;
    };

    return _setPrototypeOf(o, p);
  }

  function _objectWithoutPropertiesLoose(source, excluded) {
    if (source == null) return {};
    var target = {};
    var sourceKeys = Object.keys(source);
    var key, i;

    for (i = 0; i < sourceKeys.length; i++) {
      key = sourceKeys[i];
      if (excluded.indexOf(key) >= 0) continue;
      target[key] = source[key];
    }

    return target;
  }

  function _objectWithoutProperties(source, excluded) {
    if (source == null) return {};

    var target = _objectWithoutPropertiesLoose(source, excluded);

    var key, i;

    if (Object.getOwnPropertySymbols) {
      var sourceSymbolKeys = Object.getOwnPropertySymbols(source);

      for (i = 0; i < sourceSymbolKeys.length; i++) {
        key = sourceSymbolKeys[i];
        if (excluded.indexOf(key) >= 0) continue;
        if (!Object.prototype.propertyIsEnumerable.call(source, key)) continue;
        target[key] = source[key];
      }
    }

    return target;
  }

  function _assertThisInitialized(self) {
    if (self === void 0) {
      throw new ReferenceError("this hasn't been initialised - super() hasn't been called");
    }

    return self;
  }

  function _possibleConstructorReturn(self, call) {
    if (call && (typeof call === "object" || typeof call === "function")) {
      return call;
    }

    return _assertThisInitialized(self);
  }

  function _slicedToArray(arr, i) {
    return _arrayWithHoles(arr) || _iterableToArrayLimit(arr, i) || _nonIterableRest();
  }

  function _arrayWithHoles(arr) {
    if (Array.isArray(arr)) return arr;
  }

  function _iterableToArrayLimit(arr, i) {
    var _arr = [];
    var _n = true;
    var _d = false;
    var _e = undefined;

    try {
      for (var _i = arr[Symbol.iterator](), _s; !(_n = (_s = _i.next()).done); _n = true) {
        _arr.push(_s.value);

        if (i && _arr.length === i) break;
      }
    } catch (err) {
      _d = true;
      _e = err;
    } finally {
      try {
        if (!_n && _i["return"] != null) _i["return"]();
      } finally {
        if (_d) throw _e;
      }
    }

    return _arr;
  }

  function _nonIterableRest() {
    throw new TypeError("Invalid attempt to destructure non-iterable instance");
  }

  function styleInject(css, ref) {
    if ( ref === void 0 ) ref = {};
    var insertAt = ref.insertAt;

    if (!css || typeof document === 'undefined') { return; }

    var head = document.head || document.getElementsByTagName('head')[0];
    var style = document.createElement('style');
    style.type = 'text/css';

    if (insertAt === 'top') {
      if (head.firstChild) {
        head.insertBefore(style, head.firstChild);
      } else {
        head.appendChild(style);
      }
    } else {
      head.appendChild(style);
    }

    if (style.styleSheet) {
      style.styleSheet.cssText = css;
    } else {
      style.appendChild(document.createTextNode(css));
    }
  }

  var css = ".pickadate--element {\n  position: relative;\n  display: flex;\n  flex-direction: column;\n  align-items: flex-start;\n  box-sizing: border-box;\n}\n.pickadate--element:focus {\n  z-index: 1;\n}\ninput.pickadate--element,\nbutton.pickadate--element {\n  font: inherit;\n  color: inherit;\n}\n.pickadate--element[hidden] {\n  display: none;\n}\n\n.pickadate--root {\n  font-size: 16px;\n  font-weight: 300;\n  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'Roboto', 'Oxygen',\n    'Ubuntu', 'Cantarell', 'Fira Sans', 'Droid Sans', 'Helvetica Neue',\n    sans-serif;\n  -webkit-font-smoothing: antialiased;\n  -moz-osx-font-smoothing: grayscale;\n\n  max-width: 23em;\n  min-width: 20em;\n  border: 1px solid #ccc;\n  border-radius: 8px;\n  background-color: #fff;\n}\n.pickadate--header,\n.pickadate--body,\n.pickadate--footer {\n  align-self: stretch;\n}\n.pickadate--header {\n  flex-direction: row;\n  align-items: center;\n  padding: 0.5em 0.5em 0;\n}\n.pickadate--body {\n  flex-grow: 1;\n  padding: 0 0.125em;\n}\n.pickadate--footer {\n  margin-top: 0.325em;\n}\n\n.pickadate--monthAndYear {\n  flex-grow: 1;\n  flex-direction: row;\n  font-size: 1.125em;\n}\n.pickadate--monthAndYear_month {\n  margin-left: 0.5em;\n  margin-right: 0.325em;\n  font-weight: 500;\n}\n.pickadate--monthAndYear_year {\n  color: #999;\n}\n\n.pickadate--button {\n  border: 0;\n  padding: 0;\n  justify-content: center;\n  align-items: center;\n}\n.pickadate--button__previous,\n.pickadate--button__today,\n.pickadate--button__next {\n  height: 2.5em;\n  width: 2.5em;\n  border: 1px solid transparent;\n  border-radius: 4px;\n}\n.pickadate--button__previous svg,\n.pickadate--button__today svg,\n.pickadate--button__next svg {\n  height: 1.5em;\n}\n.pickadate--button__previous:hover svg,\n.pickadate--button__today:hover svg,\n.pickadate--button__next:hover svg,\n.pickadate--button__previous:active svg,\n.pickadate--button__today:active svg,\n.pickadate--button__next:active svg {\n  fill: #0474c5;\n}\n.pickadate--button__previous:focus,\n.pickadate--button__today:focus,\n.pickadate--button__next:focus {\n  outline: none;\n  border-color: #0474c5;\n}\n.pickadate--button__meridiem {\n  width: 3em;\n  border-radius: 4px;\n  border: 1px solid transparent;\n}\n.pickadate--button__meridiem:hover,\n.pickadate--button__meridiem:active {\n  background: #e5e5e5;\n}\n.pickadate--button__meridiem:focus {\n  border-color: #0474c5;\n  outline: none;\n}\n\n.pickadate--grid {\n  padding: 0;\n  margin: 0;\n  border: 1px solid transparent;\n  flex-grow: 1;\n  align-self: stretch;\n  align-items: stretch;\n}\n.pickadate--grid:focus {\n  outline: none;\n}\n.pickadate--grid_row {\n  flex-direction: row;\n  flex-grow: 1;\n  min-height: 2.25em;\n}\n.pickadate--grid_row__label {\n  flex-grow: 0;\n  min-height: 1.325em;\n}\n.pickadate--grid_label,\n.pickadate--grid_cell {\n  justify-content: center;\n  align-items: center;\n  align-self: stretch;\n  flex-grow: 1;\n  flex-shrink: 0;\n  flex-basis: 0;\n  min-width: 2.25em;\n}\n.pickadate--grid_label {\n  align-self: flex-start;\n  font-size: 0.75em;\n  color: #999;\n  font-weight: 700;\n}\n.pickadate--grid_cell {\n  font-size: 1.125em;\n  font-weight: 400;\n  line-height: 1;\n}\n.pickadate--grid_cell > div {\n  justify-content: center;\n  align-items: center;\n  width: 2.25em;\n  height: 2.25em;\n  border-radius: 50%;\n}\n.pickadate--grid_cell:hover > div,\n.pickadate--grid:focus:not(:active)\n  .pickadate--grid_cell__highlighted:not(.pickadate--grid_cell__selected)\n  > div,\n.pickadate--grid__focused:not(:active)\n  .pickadate--grid_cell__highlighted:not(.pickadate--grid_cell__selected)\n  > div {\n  background: #bde1fa;\n}\n.pickadate--grid_cell__today > div {\n  border: 1px solid #e5e5e5;\n}\n.pickadate--grid_cell__today:hover:not(.pickadate--grid_cell__disabled) > div,\n.pickadate--grid:focus\n  .pickadate--grid_cell__today.pickadate--grid_cell__highlighted:not(.pickadate--grid_cell__selected)\n  > div,\n.pickadate--grid__focused\n  .pickadate--grid_cell__today.pickadate--grid_cell__highlighted:not(.pickadate--grid_cell__selected)\n  > div,\n.pickadate--grid_cell__today.pickadate--grid_cell__selected > div {\n  border-color: #0474c5;\n}\n.pickadate--grid_cell__today.pickadate--grid_cell__selected > div:after {\n  content: ' ';\n  position: absolute;\n  top: 0;\n  bottom: 0;\n  right: 0;\n  left: 0;\n  border: 1px solid #fff;\n  border-radius: 50%;\n}\n.pickadate--grid_cell__selected > div,\n.pickadate--grid_cell__selected:hover > div {\n  background: #0474c5;\n  color: #fff;\n  font-weight: 600;\n}\n.pickadate--grid_cell__disabled,\n.pickadate--grid_cell__outOfView {\n  opacity: 0.25;\n  transform: scale(0.8);\n}\n.pickadate--grid_cell__disabled > div,\n.pickadate--grid_cell__disabled:hover > div {\n  background: #b2b2b2;\n}\n\n.pickadate--time {\n  flex-direction: row;\n  align-items: stretch;\n  align-self: stretch;\n  justify-content: center;\n\n  border-top: 1px solid #e5e5e5;\n  margin: 0 17.5% 0.325em;\n  padding-top: 0.25em;\n\n  font-weight: 500;\n}\n.pickadate--time_unit {\n}\n.pickadate--time_unit__hours {\n  margin-right: 0.5em;\n}\n.pickadate--time_unit__minutes {\n  margin-right: 0.25em;\n}\n.pickadate--time_separator {\n  margin-right: 0.25em;\n  justify-content: center;\n  font-weight: bold;\n}\n.pickadate--time_counter {\n  position: absolute;\n  left: 2px;\n  opacity: 0;\n\n  border: 0;\n  border-radius: 4px;\n  padding: 0 4px;\n  height: calc(50% - 3px);\n  justify-content: center;\n}\n.pickadate--time_counter:hover {\n  background: #e5e5e5;\n}\n.pickadate--time_unit:hover .pickadate--time_counter {\n  opacity: 1;\n}\n.pickadate--time_counter__up {\n  top: 2px;\n}\n.pickadate--time_counter__down {\n  bottom: 2px;\n}\n.pickadate--time_counter svg {\n  height: 0.4375em;\n}\n.pickadate--time_input {\n  width: 4em;\n  height: 2.25em;\n  padding: 0 0.25em 0 1.5em;\n  border: 1px solid transparent;\n  border-radius: 6px;\n}\n.pickadate--time_input:focus {\n  border-color: #0474c5;\n  outline: none;\n}\n.pickadate--time_input:focus ~ .pickadate--time_counter {\n  z-index: 2;\n  opacity: 1;\n}\n\n/**\n * INPUT PICKER\n */\n\n.pickadate--input-root {\n  position: absolute;\n  z-index: 1;\n}\n.pickadate--input-root .pickadate--root {\n  max-height: 0;\n  overflow: hidden;\n  opacity: 0;\n  pointer-events: none;\n}\n.pickadate--input-root .pickadate--root__active {\n  max-height: none;\n  opacity: 1;\n  pointer-events: auto;\n}\n";
  styleInject(css);

  // NUMBERS //
  /////////////

  var padZero = function padZero(number, digitsCount) {
    number = "".concat(number);
    var numberDigitsCount = number.length;
    var differenceInDigitsCount = digitsCount - numberDigitsCount;
    return differenceInDigitsCount > 0 ? '0'.repeat(differenceInDigitsCount) + number : number;
  }; /////////////
  // OBJECTS //
  /////////////

  var mergeUpdates = function mergeUpdates(defaults, updates) {
    var mergedObject = _objectSpread({}, defaults);

    Object.keys(defaults).forEach(function (key) {
      var defaultValue = defaults[key];
      var updateValue = updates[key]; // If there is no default, remove the key

      if (defaultValue === undefined) {
        delete mergedObject[key];
        return;
      } // If the default is the same as the update, do nothing


      if (defaultValue === updateValue) {
        return;
      } // If both values are objects, recursively merge them


      if (defaultValue && updateValue && defaultValue.constructor === Object && updateValue.constructor === Object) {
        mergedObject[key] = mergeUpdates(defaultValue, updateValue);
        return;
      }

      mergedObject[key] = updates.hasOwnProperty(key) ? updateValue : defaultValue;
    });
    return mergedObject;
  };
  var copyDefinedValues = function copyDefinedValues(object) {
    return Object.keys(object).reduce(function (accumulator, key) {
      var value = object[key];

      if (value !== undefined) {
        accumulator[key] = value;
      }

      return accumulator;
    }, {});
  }; ////////////
  // ARRAYS //
  ////////////

  var createRange = function createRange(fromIndex, toIndex) {
    var range = [];

    for (var index = fromIndex; index <= toIndex; index++) {
      range.push(index);
    }

    return range;
  };

  var DAYS_IN_WEEK = 7;
  /**
   * Gets the weekday labels.
   */

  var getWeekdayLabels = function getWeekdayLabels(weekdays, firstDayOfWeek) {
    return weekdays.map(function (_, dayIndex) {
      return weekdays[(dayIndex + firstDayOfWeek) % DAYS_IN_WEEK];
    });
  };
  /**
   * Gets the dates in weeks.
   */

  var getDatesInWeeks = function getDatesInWeeks(originDateObject, firstDayOfWeek) {
    var year = originDateObject.getFullYear();
    var month = originDateObject.getMonth();
    var monthOfLastDayInFirstWeek = getMonthOfLastDayInFirstWeek(year, month, firstDayOfWeek);
    var shift = monthOfLastDayInFirstWeek === month ? 0 : -1;
    var start = 0 + shift;
    var end = 5 + shift;
    return createRange(start, end).map(function (weekIndex) {
      return getDatesInWeek(year, month, weekIndex, firstDayOfWeek);
    });
  };
  /**
   * Gets the dates in a week.
   */

  var getDatesInWeek = function getDatesInWeek(year, month, weekIndex, firstDayOfWeek) {
    var startDayOfMonth = getStartDayOfMonth(year, month);
    var startDayOfWeek = weekIndex * DAYS_IN_WEEK;
    var start = 1;
    var end = 7;
    return createRange(start, end).map(function (date) {
      return new Date(year, month, date + startDayOfWeek - startDayOfMonth + firstDayOfWeek);
    });
  };
  /**
   * Gets the month of the last day in the first week of a month.
   */

  var getMonthOfLastDayInFirstWeek = function getMonthOfLastDayInFirstWeek(year, month, firstDayOfWeek) {
    var startDayOfMonth = getStartDayOfMonth(year, month) - firstDayOfWeek;
    var date = DAYS_IN_WEEK - startDayOfMonth;

    if (startDayOfMonth < 0) {
      return new Date(year, month, firstDayOfWeek - date).getMonth();
    }

    return new Date(year, month, date).getMonth();
  };
  /**
   * Gets the start day of a month.
   */

  var getStartDayOfMonth = function getStartDayOfMonth(year, month) {
    var startDateOfMonth = new Date(year, month, 1);
    return startDateOfMonth.getDay();
  };
  /**
   * Gets the start date of a month, give a date.
   */

  var getStartDateOfMonth = function getStartDateOfMonth(dateObject) {
    var year = dateObject.getFullYear();
    var month = dateObject.getMonth();
    return new Date(year, month, 1);
  };

  var global$1 = (typeof global !== "undefined" ? global :
              typeof self !== "undefined" ? self :
              typeof window !== "undefined" ? window : {});

  // shim for using process in browser
  // based off https://github.com/defunctzombie/node-process/blob/master/browser.js

  function defaultSetTimout() {
      throw new Error('setTimeout has not been defined');
  }
  function defaultClearTimeout () {
      throw new Error('clearTimeout has not been defined');
  }
  var cachedSetTimeout = defaultSetTimout;
  var cachedClearTimeout = defaultClearTimeout;
  if (typeof global$1.setTimeout === 'function') {
      cachedSetTimeout = setTimeout;
  }
  if (typeof global$1.clearTimeout === 'function') {
      cachedClearTimeout = clearTimeout;
  }

  function runTimeout(fun) {
      if (cachedSetTimeout === setTimeout) {
          //normal enviroments in sane situations
          return setTimeout(fun, 0);
      }
      // if setTimeout wasn't available but was latter defined
      if ((cachedSetTimeout === defaultSetTimout || !cachedSetTimeout) && setTimeout) {
          cachedSetTimeout = setTimeout;
          return setTimeout(fun, 0);
      }
      try {
          // when when somebody has screwed with setTimeout but no I.E. maddness
          return cachedSetTimeout(fun, 0);
      } catch(e){
          try {
              // When we are in I.E. but the script has been evaled so I.E. doesn't trust the global object when called normally
              return cachedSetTimeout.call(null, fun, 0);
          } catch(e){
              // same as above but when it's a version of I.E. that must have the global object for 'this', hopfully our context correct otherwise it will throw a global error
              return cachedSetTimeout.call(this, fun, 0);
          }
      }


  }
  function runClearTimeout(marker) {
      if (cachedClearTimeout === clearTimeout) {
          //normal enviroments in sane situations
          return clearTimeout(marker);
      }
      // if clearTimeout wasn't available but was latter defined
      if ((cachedClearTimeout === defaultClearTimeout || !cachedClearTimeout) && clearTimeout) {
          cachedClearTimeout = clearTimeout;
          return clearTimeout(marker);
      }
      try {
          // when when somebody has screwed with setTimeout but no I.E. maddness
          return cachedClearTimeout(marker);
      } catch (e){
          try {
              // When we are in I.E. but the script has been evaled so I.E. doesn't  trust the global object when called normally
              return cachedClearTimeout.call(null, marker);
          } catch (e){
              // same as above but when it's a version of I.E. that must have the global object for 'this', hopfully our context correct otherwise it will throw a global error.
              // Some versions of I.E. have different rules for clearTimeout vs setTimeout
              return cachedClearTimeout.call(this, marker);
          }
      }



  }
  var queue = [];
  var draining = false;
  var currentQueue;
  var queueIndex = -1;

  function cleanUpNextTick() {
      if (!draining || !currentQueue) {
          return;
      }
      draining = false;
      if (currentQueue.length) {
          queue = currentQueue.concat(queue);
      } else {
          queueIndex = -1;
      }
      if (queue.length) {
          drainQueue();
      }
  }

  function drainQueue() {
      if (draining) {
          return;
      }
      var timeout = runTimeout(cleanUpNextTick);
      draining = true;

      var len = queue.length;
      while(len) {
          currentQueue = queue;
          queue = [];
          while (++queueIndex < len) {
              if (currentQueue) {
                  currentQueue[queueIndex].run();
              }
          }
          queueIndex = -1;
          len = queue.length;
      }
      currentQueue = null;
      draining = false;
      runClearTimeout(timeout);
  }
  function nextTick(fun) {
      var args = new Array(arguments.length - 1);
      if (arguments.length > 1) {
          for (var i = 1; i < arguments.length; i++) {
              args[i - 1] = arguments[i];
          }
      }
      queue.push(new Item(fun, args));
      if (queue.length === 1 && !draining) {
          runTimeout(drainQueue);
      }
  }
  // v8 likes predictible objects
  function Item(fun, array) {
      this.fun = fun;
      this.array = array;
  }
  Item.prototype.run = function () {
      this.fun.apply(null, this.array);
  };
  var title = 'browser';
  var platform = 'browser';
  var browser = true;
  var env = {};
  var argv = [];
  var version = ''; // empty string to avoid regexp issues
  var versions = {};
  var release = {};
  var config = {};

  function noop() {}

  var on = noop;
  var addListener = noop;
  var once = noop;
  var off = noop;
  var removeListener = noop;
  var removeAllListeners = noop;
  var emit = noop;

  function binding(name) {
      throw new Error('process.binding is not supported');
  }

  function cwd () { return '/' }
  function chdir (dir) {
      throw new Error('process.chdir is not supported');
  }function umask() { return 0; }

  // from https://github.com/kumavis/browser-process-hrtime/blob/master/index.js
  var performance$1 = global$1.performance || {};
  var performanceNow =
    performance$1.now        ||
    performance$1.mozNow     ||
    performance$1.msNow      ||
    performance$1.oNow       ||
    performance$1.webkitNow  ||
    function(){ return (new Date()).getTime() };

  // generate timestamp or delta
  // see http://nodejs.org/api/process.html#process_process_hrtime
  function hrtime(previousTimestamp){
    var clocktime = performanceNow.call(performance$1)*1e-3;
    var seconds = Math.floor(clocktime);
    var nanoseconds = Math.floor((clocktime%1)*1e9);
    if (previousTimestamp) {
      seconds = seconds - previousTimestamp[0];
      nanoseconds = nanoseconds - previousTimestamp[1];
      if (nanoseconds<0) {
        seconds--;
        nanoseconds += 1e9;
      }
    }
    return [seconds,nanoseconds]
  }

  var startTime = new Date();
  function uptime() {
    var currentTime = new Date();
    var dif = currentTime - startTime;
    return dif / 1000;
  }

  var process = {
    nextTick: nextTick,
    title: title,
    browser: browser,
    env: env,
    argv: argv,
    version: version,
    versions: versions,
    on: on,
    addListener: addListener,
    once: once,
    off: off,
    removeListener: removeListener,
    removeAllListeners: removeAllListeners,
    emit: emit,
    binding: binding,
    cwd: cwd,
    chdir: chdir,
    umask: umask,
    hrtime: hrtime,
    platform: platform,
    release: release,
    config: config,
    uptime: uptime
  };

  // CHECKERS //
  //////////////

  var isSameDate = function isSameDate(one, two) {
    return one instanceof Date && two instanceof Date && isSameMonth(one, two) && one.getDate() === two.getDate();
  };
  var isSameMonth = function isSameMonth(one, two) {
    return one instanceof Date && two instanceof Date && isSameYear(one, two) && one.getMonth() === two.getMonth();
  };
  var isSameYear = function isSameYear(one, two) {
    return one instanceof Date && two instanceof Date && one.getFullYear() === two.getFullYear();
  };
  var isBeforeDate = function isBeforeDate(one, two) {
    return one instanceof Date && two instanceof Date && (one.getFullYear() < two.getFullYear() || isSameYear(one, two) && one.getMonth() < two.getMonth() || isSameMonth(one, two) && one.getDate() < two.getDate());
  };
  var isSameOrBeforeDate = function isSameOrBeforeDate(one, two) {
    return isSameDate(one, two) || isBeforeDate(one, two);
  }; ////////////
  // FORMAT //
  ////////////

  /**
   * A mapping of template hooks to formatters.
   */

  var HOOK_FORMATTER = {
    D: function D(dateObject) {
      return "".concat(dateObject.getDate());
    },
    DD: function DD(dateObject) {
      return "".concat(padZero(HOOK_FORMATTER.D(dateObject), 2));
    },
    DDD: function DDD(dateObject, words) {
      return words[dateObject.getDay()];
    },
    DDDD: function DDDD(dateObject, words) {
      return words[dateObject.getDay()];
    },
    M: function M(dateObject) {
      return "".concat(dateObject.getMonth() + 1);
    },
    MM: function MM(dateObject) {
      return "".concat(padZero(HOOK_FORMATTER.M(dateObject), 2));
    },
    MMM: function MMM(dateObject, words) {
      return words[dateObject.getMonth()];
    },
    MMMM: function MMMM(dateObject, words) {
      return words[dateObject.getMonth()];
    },
    YYYY: function YYYY(dateObject) {
      return "".concat(dateObject.getFullYear());
    },
    H: function H(dateObject) {
      return "".concat(dateObject.getHours());
    },
    HH: function HH(dateObject) {
      return "".concat(padZero(HOOK_FORMATTER.H(dateObject), 2));
    },
    h: function h(dateObject) {
      return "".concat(dateObject.getHours() % 12 || 12);
    },
    hh: function hh(dateObject) {
      return "".concat(padZero(HOOK_FORMATTER.h(dateObject), 2));
    },
    m: function m(dateObject) {
      return "".concat(dateObject.getMinutes());
    },
    mm: function mm(dateObject) {
      return "".concat(padZero(HOOK_FORMATTER.m(dateObject), 2));
    },
    a: function a(dateObject) {
      return dateObject.getHours() < 12 ? 'a.m.' : 'p.m.';
    },
    A: function A(dateObject) {
      return dateObject.getHours() < 12 ? 'AM' : 'PM';
    },
    s: function s(dateObject) {
      return "".concat(dateObject.getSeconds());
    },
    ss: function ss(dateObject) {
      return "".concat(padZero(HOOK_FORMATTER.s(dateObject), 2));
    },
    x: function x(dateObject) {
      return "".concat(dateObject.getTime());
    }
  };

  var getStartingDigits = function getStartingDigits(string) {
    return string.replace(/(^\d*)(.*)/, '$1');
  };

  var getStartingWord = function getStartingWord(string) {
    return string.replace(/(^\w*)(.*)/, '$1');
  };

  var getStartingLowerCaseMeridiem = function getStartingLowerCaseMeridiem(string) {
    return string.replace(/(^[ap]\.m\.)?(.*)/, '$1');
  };

  var getStartingUpperCaseMeridiem = function getStartingUpperCaseMeridiem(string) {
    return string.replace(/(^[AP]M)?(.*)/, '$1');
  };
  /**
   * A mapping of template hooks to parsers.
   */


  var HOOK_PARSER = {
    D: getStartingDigits,
    DD: getStartingDigits,
    DDD: getStartingWord,
    DDDD: getStartingWord,
    M: getStartingDigits,
    MM: getStartingDigits,
    MMM: getStartingWord,
    MMMM: getStartingWord,
    YYYY: getStartingDigits,
    H: getStartingDigits,
    HH: getStartingDigits,
    h: getStartingDigits,
    hh: getStartingDigits,
    m: getStartingDigits,
    mm: getStartingDigits,
    a: getStartingLowerCaseMeridiem,
    A: getStartingUpperCaseMeridiem,
    s: getStartingDigits,
    ss: getStartingDigits,
    x: getStartingDigits
  };
  {
    /* istanbul ignore next */
    if (process.env.NODE_ENV === 'development') {
      var extraFormatterKeys = Object.keys(HOOK_FORMATTER).filter(function (key) {
        return !HOOK_PARSER[key];
      });
      console.assert(!extraFormatterKeys.length, 'Missing keys to parse with', extraFormatterKeys);
      var extraParserKeys = Object.keys(HOOK_PARSER).filter(function (key) {
        return !HOOK_FORMATTER[key];
      });
      console.assert(!extraParserKeys.length, 'Missing keys to format with', extraParserKeys);
    }
  }
  /**
   * A collection of the template hooks.
   */

  var HOOKS = Object.keys(HOOK_FORMATTER);
  /**
   * A regular expression that matches all segments of a template string.
   */

  var HOOKS_REGEXP = new RegExp([// Match any characters escaped with square brackets
  '(\\[.*?\\])', // Match any template hooks
  "(?:\\b(".concat(HOOKS.join('|'), ")\\b)"), // Match all other characters
  '(.)'].join('|'), 'g');
  var format = function format(dateObject, template, templateHookWords) {
    return (// Match hooks within the template
      matchHooks(template) // Map through the matches while formatting template hooks
      // and removing the hooks of escaped characters
      .map(function (match) {
        if (match.index === 2) {
          var formatter = HOOK_FORMATTER[match.chunk];
          return formatter(dateObject, templateHookWords[match.chunk]);
        }

        if (match.index === 1) {
          return match.chunk.replace(/^\[(.*)]$/, '$1');
        }

        return match.chunk;
      }) // Join the chunks together into a string
      .join('')
    );
  };

  var matchHooks = function matchHooks(template) {
    return template // Split the template using the regular expression
    .split(HOOKS_REGEXP) // Map the chunks to keep a reference of their match group index
    .map(function (chunk, index) {
      return {
        chunk: chunk,
        index: index % 4
      };
    }) // Filter out false-y chunks
    .filter(function (match) {
      return !!match.chunk;
    });
  };

  var isDisabled = function isDisabled(state, dateObject) {
    var minimum = state.minimum,
        maximum = state.maximum,
        disabled = state.disabled;
    return isBeforeDate(dateObject, minimum) || isBeforeDate(maximum, dateObject) || disabled.some(function (value) {
      if (typeof value === 'number') {
        return dateObject.getDay() === value;
      }

      if (value instanceof Date) {
        return isSameDate(dateObject, value);
      }

      if (Array.isArray(value)) {
        var _value = _slicedToArray(value, 2),
            fromDate = _value[0],
            toDate = _value[1];

        return isSameOrBeforeDate(fromDate, dateObject) && isSameOrBeforeDate(dateObject, toDate);
      }
    });
  };

  var computeInfo = function computeInfo(state, dateObject) {
    return {
      isToday: isSameDate(new Date(), dateObject),
      isSelected: isSameDate(state.selected, dateObject),
      isHighlighted: isSameDate(state.highlighted, dateObject),
      isSameMonth: isSameMonth(state.view, dateObject),
      isDisabled: isDisabled(state, dateObject)
    };
  };

  var KEY_CODE = {
    BACKSPACE: 8,
    ENTER: 13,
    SPACE: 32,
    LEFT: 37,
    UP: 38,
    RIGHT: 39,
    DOWN: 40
  };
  var ARROW_KEY_CODES = Object.values(KEY_CODE);

  var createOnKeyDownCalendar = function createOnKeyDownCalendar(picker) {
    return function onKeyDownCalendar(event) {
      if (event.keyCode === KEY_CODE.BACKSPACE) {
        event.preventDefault();
        picker.clear();
      } else if (event.keyCode === KEY_CODE.ENTER || event.keyCode === KEY_CODE.SPACE) {
        event.preventDefault();
        picker.setSelected({
          value: picker.store.getState().highlighted
        });
      } else if (ARROW_KEY_CODES.includes(event.keyCode)) {
        event.preventDefault();
        picker.setHighlighted({
          keyCode: event.keyCode
        });
      }
    };
  };

  var CLASS_NAME_MAP = {
    inputRoot: 'pickadate--input-root',
    root: 'pickadate--root',
    root__active: 'pickadate--root__active',
    header: 'pickadate--header',
    body: 'pickadate--body',
    footer: 'pickadate--footer',
    monthAndYear: 'pickadate--monthAndYear',
    monthAndYear_month: 'pickadate--monthAndYear_month',
    monthAndYear_year: 'pickadate--monthAndYear_year',
    grid: 'pickadate--grid',
    grid__focused: 'pickadate--grid__focused',
    grid_row: 'pickadate--grid_row',
    grid_row__label: 'pickadate--grid_row__label',
    grid_label: 'pickadate--grid_label',
    grid_cell: 'pickadate--grid_cell',
    grid_cell__today: 'pickadate--grid_cell__today',
    grid_cell__highlighted: 'pickadate--grid_cell__highlighted',
    grid_cell__selected: 'pickadate--grid_cell__selected',
    grid_cell__outOfView: 'pickadate--grid_cell__outOfView',
    grid_cell__disabled: 'pickadate--grid_cell__disabled',
    button_previous: ['pickadate--button', 'pickadate--button__previous'],
    button_today: ['pickadate--button', 'pickadate--button__today'],
    button_next: ['pickadate--button', 'pickadate--button__next'],
    button_clear: ['pickadate--button', 'pickadate--button__clear'],
    button_meridiem: ['pickadate--button', 'pickadate--button__meridiem'],
    time: 'pickadate--time',
    time_hours: ['pickadate--time_unit', 'pickadate--time_unit__hours'],
    time_minutes: ['pickadate--time_unit', 'pickadate--time_unit__minutes'],
    time_separator: 'pickadate--time_separator',
    time_input: 'pickadate--time_input',
    time_input__hours: 'pickadate--time_input__hours',
    time_input__minutes: 'pickadate--time_input__minutes',
    time_counter: 'pickadate--time_counter',
    time_counter__up: 'pickadate--time_counter__up',
    time_counter__down: 'pickadate--time_counter__down'
  };

  var View = function View() {
    return null;
  };

  var Button = function Button() {
    return null;
  };

  var Input = function Input() {
    return null;
  };

  var setComponents = function setComponents(_ref) {
    var ButtonComponent = _ref.ButtonComponent,
        ViewComponent = _ref.ViewComponent,
        InputComponent = _ref.InputComponent;
    Button = ButtonComponent;
    View = ViewComponent;
    Input = InputComponent;
  };
  var RootBox =
  /*#__PURE__*/
  function (_React$Component) {
    _inherits(RootBox, _React$Component);

    function RootBox() {
      _classCallCheck(this, RootBox);

      return _possibleConstructorReturn(this, _getPrototypeOf(RootBox).apply(this, arguments));
    }

    _createClass(RootBox, [{
      key: "render",
      value: function render() {
        var _this$props = this.props,
            children = _this$props.children,
            isOpenForInput = _this$props.isOpenForInput;
        var styleName = ['root'];

        if (isOpenForInput) {
          styleName.push('root__active');
        }

        return React.createElement(View, {
          styleName: styleName
        }, children);
      }
    }]);

    return RootBox;
  }(React.Component);
  var HeaderBox = function HeaderBox(_ref2) {
    var children = _ref2.children;
    return React.createElement(View, {
      styleName: "header"
    }, children);
  };
  var BodyBox = function BodyBox(_ref3) {
    var children = _ref3.children;
    return React.createElement(View, {
      styleName: "body"
    }, children);
  };
  var FooterBox = function FooterBox(_ref4) {
    var children = _ref4.children;
    return React.createElement(View, {
      styleName: "footer"
    }, children);
  };
  var MonthAndYearBox = function MonthAndYearBox(_ref5) {
    var picker = _ref5.picker;
    var state = picker.store.getState();
    return React.createElement(View, {
      styleName: "monthAndYear"
    }, React.createElement(View, {
      styleName: "monthAndYear_month"
    }, state.templateHookWords.MMMM[state.view.getMonth()]), React.createElement(View, {
      styleName: "monthAndYear_year"
    }, state.view.getFullYear()));
  };
  var TimeBox = function TimeBox(_ref6) {
    var children = _ref6.children,
        picker = _ref6.picker;
    var state = picker.store.getState();
    return React.createElement(View, {
      styleName: "time",
      isHidden: !state.selected
    }, children);
  };
  var PreviousButton = function PreviousButton(_ref7) {
    var picker = _ref7.picker,
        renderIcon = _ref7.renderIcon;
    return React.createElement(Button, {
      styleName: "button_previous",
      onPress: function onPress() {
        return picker.viewPrevious();
      },
      picker: picker
    }, renderIcon());
  };
  var TodayButton = function TodayButton(_ref8) {
    var picker = _ref8.picker,
        renderIcon = _ref8.renderIcon;
    return React.createElement(Button, {
      styleName: "button_today",
      onPress: function onPress() {
        return picker.viewToday();
      },
      picker: picker
    }, renderIcon());
  };
  var NextButton = function NextButton(_ref9) {
    var picker = _ref9.picker,
        renderIcon = _ref9.renderIcon;
    return React.createElement(Button, {
      styleName: "button_next",
      onPress: function onPress() {
        return picker.viewNext();
      },
      picker: picker
    }, renderIcon());
  };
  var GridButton =
  /*#__PURE__*/
  function (_React$Component2) {
    _inherits(GridButton, _React$Component2);

    function GridButton() {
      var _getPrototypeOf2;

      var _this;

      _classCallCheck(this, GridButton);

      for (var _len = arguments.length, args = new Array(_len), _key = 0; _key < _len; _key++) {
        args[_key] = arguments[_key];
      }

      _this = _possibleConstructorReturn(this, (_getPrototypeOf2 = _getPrototypeOf(GridButton)).call.apply(_getPrototypeOf2, [this].concat(args)));
      _this.onKeyDown = createOnKeyDownCalendar(_this.props.picker);
      return _this;
    }

    _createClass(GridButton, [{
      key: "render",
      value: function render() {
        var _this$props2 = this.props,
            picker = _this$props2.picker,
            onPress = _this$props2.onPress,
            renderCell = _this$props2.renderCell,
            isRemoteActive = _this$props2.isRemoteActive;
        var state = picker.store.getState();
        var datesInWeeks = getDatesInWeeks(state.view, state.firstDayOfWeek);
        var styleName = ['grid'];

        if (isRemoteActive) {
          styleName.push('grid__focused');
        }

        return React.createElement(Button, {
          styleName: styleName,
          onPress: onPress,
          picker: picker,
          onKeyDown: this.onKeyDown
        }, React.createElement(GridRow, {
          isLabel: true
        }, getWeekdayLabels(state.templateHookWords.DDD, state.firstDayOfWeek).map(function (weekday) {
          return React.createElement(View, {
            styleName: "grid_label",
            key: weekday
          }, weekday);
        })), datesInWeeks.map(function (datesInWeek, index) {
          return React.createElement(GridRow, {
            key: index,
            isLabel: false
          }, datesInWeek.map(function (dateObject) {
            return React.createElement(GridCell, {
              key: dateObject.getTime(),
              state: state,
              dateObject: dateObject,
              renderCell: renderCell
            });
          }));
        }));
      }
    }]);

    return GridButton;
  }(React.Component);

  var GridRow = function GridRow(_ref11) {
    var children = _ref11.children,
        isLabel = _ref11.isLabel;
    return React.createElement(View, {
      styleName: ['grid_row', isLabel ? 'grid_row__label' : undefined]
    }, children);
  };

  var GridCell = function GridCell(_ref12) {
    var children = _ref12.children,
        state = _ref12.state,
        dateObject = _ref12.dateObject,
        renderCell = _ref12.renderCell;
    var cellInfo = computeInfo(state, dateObject);
    var styleName = ['grid_cell'];

    if (cellInfo.isToday) {
      styleName.push('grid_cell__today');
    }

    if (cellInfo.isHighlighted) {
      styleName.push('grid_cell__highlighted');
    }

    if (cellInfo.isSelected) {
      styleName.push('grid_cell__selected');
    }

    if (!cellInfo.isSameMonth) {
      styleName.push('grid_cell__outOfView');
    }

    if (cellInfo.isDisabled) {
      styleName.push('grid_cell__disabled');
    }

    return React.createElement(View, {
      styleName: styleName,
      dataset: {
        value: dateObject.getTime()
      }
    }, renderCell({
      dateObject: dateObject,
      children: React.createElement(View, null, dateObject.getDate())
    }));
  };

  var HourInput = function HourInput(_ref13) {
    var picker = _ref13.picker,
        renderIconUp = _ref13.renderIconUp,
        renderIconDown = _ref13.renderIconDown;
    var state = picker.store.getState();
    var hours = state.selected ? padZero(state.selected.getHours() % 12 || 12, 2) : '';
    return React.createElement(View, {
      styleName: "time_hours"
    }, React.createElement(Input, {
      styleName: ['time_input', 'time_input__hours'],
      value: hours,
      readOnly: true
    }), React.createElement(TimeCounters, {
      picker: picker,
      onPressUp: function onPressUp() {
        return picker.cycleHourUp();
      },
      onPressDown: function onPressDown() {
        return picker.cycleHourDown();
      },
      renderIconUp: renderIconUp,
      renderIconDown: renderIconDown
    }));
  };
  var MinuteInput = function MinuteInput(_ref14) {
    var picker = _ref14.picker,
        renderIconUp = _ref14.renderIconUp,
        renderIconDown = _ref14.renderIconDown;
    var state = picker.store.getState();
    var minutes = state.selected ? padZero(state.selected.getMinutes(), 2) : '';
    return React.createElement(View, {
      styleName: "time_minutes"
    }, React.createElement(Input, {
      styleName: ['time_input', 'time_input__minutes'],
      value: minutes,
      readOnly: true
    }), React.createElement(TimeCounters, {
      picker: picker,
      onPressUp: function onPressUp() {
        return picker.cycleMinuteUp();
      },
      onPressDown: function onPressDown() {
        return picker.cycleMinuteDown();
      },
      renderIconUp: renderIconUp,
      renderIconDown: renderIconDown
    }));
  };

  var TimeCounters = function TimeCounters(_ref15) {
    var picker = _ref15.picker,
        onPressUp = _ref15.onPressUp,
        onPressDown = _ref15.onPressDown,
        renderIconUp = _ref15.renderIconUp,
        renderIconDown = _ref15.renderIconDown;
    return React.createElement(React.Fragment, null, React.createElement(Button, {
      styleName: ['time_counter', 'time_counter__up'],
      onPress: onPressUp,
      picker: picker,
      tabIndex: -1
    }, renderIconUp()), React.createElement(Button, {
      styleName: ['time_counter', 'time_counter__down'],
      onPress: onPressDown,
      picker: picker,
      tabIndex: -1
    }, renderIconDown()));
  };

  var TimeSeparator = function TimeSeparator() {
    return React.createElement(View, {
      styleName: "time_separator"
    }, ":");
  };
  var MeridiemButton = function MeridiemButton(_ref16) {
    var picker = _ref16.picker;

    var _picker$store$getStat = picker.store.getState(),
        selected = _picker$store$getStat.selected;

    return React.createElement(Button, {
      styleName: "button_meridiem",
      onPress: function onPress() {
        return picker.cycleMeridiem();
      },
      picker: picker
    }, selected ? selected.getHours() < 12 ? 'AM' : 'PM' : 'AM');
  };

  var flatten = function flatten(classNames) {
    var classList = Array.isArray(classNames) ? classNames : [classNames];
    return classList.reduce(function (accumulator, className) {
      if (!className) {
        return accumulator;
      }

      if (Array.isArray(className)) {
        className.forEach(function (className) {
          if (typeof className === 'string') {
            accumulator[className] = true;
          }
        });
      } else if (typeof className === 'string') {
        accumulator[className] = true;
      } else if (className.constructor === Object) {
        accumulator = _objectSpread({}, accumulator, className);
      }

      return accumulator;
    }, {});
  };
  var join = function join(classNames) {
    var classData = flatten(classNames);
    return Object.keys(classData).reduce(function (accumulator, className) {
      if (classData[className]) {
        accumulator.push(className);
      }

      return accumulator;
    }, []).join(' ');
  };

  var RENDER_OPTIONS = {
    mode: 'date',
    renderCell: function renderCell(_ref) {
      var children = _ref.children;
      return children;
    },
    className: CLASS_NAME_MAP
  };

  function getValueFromMouseEvent(event) {
    var eventPath = getEventPath(event);
    var value = getValueFromMouseEventPath(eventPath, event.currentTarget);

    if (value == null) {
      return;
    }

    value = parseInt(value, 10);

    if (!Number.isFinite(value)) {
      console.error('Unable to get value from mouse event %o', event);
      return;
    }

    return value;
  }

  var getValueFromMouseEventPath = function getValueFromMouseEventPath(path, rootNode) {
    for (var i = 0; i < path.length; i += 1) {
      var node = path[i];

      if (node === rootNode) {
        return;
      } // $FlowFixMe


      var value = node.dataset.value;

      if (value != null) {
        return value;
      }
    }
  };

  var getEventPath = function getEventPath(event) {
    // $FlowFixMe
    if (event.path) {
      return event.path;
    }

    var path = [];
    var target = event.target; // $FlowFixMe

    while (target.parentNode) {
      path.push(target); // $FlowFixMe

      target = target.parentNode;
    }

    path.push(document, window);
    return path;
  };

  var TEMPLATE_HOOK_WORDS = {
    MMM: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    MMMM: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
    DDD: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
    DDDD: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday']
  };

  var commonjsGlobal = typeof window !== 'undefined' ? window : typeof global !== 'undefined' ? global : typeof self !== 'undefined' ? self : {};

  function createCommonjsModule(fn, module) {
  	return module = { exports: {} }, fn(module, module.exports), module.exports;
  }

  var performanceNow$1 = createCommonjsModule(function (module) {
  // Generated by CoffeeScript 1.12.2
  (function() {
    var getNanoSeconds, hrtime$$1, loadTime, moduleLoadTime, nodeLoadTime, upTime;

    if ((typeof performance !== "undefined" && performance !== null) && performance.now) {
      module.exports = function() {
        return performance.now();
      };
    } else if ((typeof process !== "undefined" && process !== null) && process.hrtime) {
      module.exports = function() {
        return (getNanoSeconds() - nodeLoadTime) / 1e6;
      };
      hrtime$$1 = process.hrtime;
      getNanoSeconds = function() {
        var hr;
        hr = hrtime$$1();
        return hr[0] * 1e9 + hr[1];
      };
      moduleLoadTime = getNanoSeconds();
      upTime = process.uptime() * 1e9;
      nodeLoadTime = moduleLoadTime - upTime;
    } else if (Date.now) {
      module.exports = function() {
        return Date.now() - loadTime;
      };
      loadTime = Date.now();
    } else {
      module.exports = function() {
        return new Date().getTime() - loadTime;
      };
      loadTime = new Date().getTime();
    }

  }).call(commonjsGlobal);


  });

  var root = typeof window === 'undefined' ? commonjsGlobal : window
    , vendors = ['moz', 'webkit']
    , suffix = 'AnimationFrame'
    , raf = root['request' + suffix]
    , caf = root['cancel' + suffix] || root['cancelRequest' + suffix];

  for(var i = 0; !raf && i < vendors.length; i++) {
    raf = root[vendors[i] + 'Request' + suffix];
    caf = root[vendors[i] + 'Cancel' + suffix]
        || root[vendors[i] + 'CancelRequest' + suffix];
  }

  // Some versions of FF have rAF but not cAF
  if(!raf || !caf) {
    var last = 0
      , id$1 = 0
      , queue$1 = []
      , frameDuration = 1000 / 60;

    raf = function(callback) {
      if(queue$1.length === 0) {
        var _now = performanceNow$1()
          , next = Math.max(0, frameDuration - (_now - last));
        last = next + _now;
        setTimeout(function() {
          var cp = queue$1.slice(0);
          // Clear queue here to prevent
          // callbacks from appending listeners
          // to the current frame's queue
          queue$1.length = 0;
          for(var i = 0; i < cp.length; i++) {
            if(!cp[i].cancelled) {
              try{
                cp[i].callback(last);
              } catch(e) {
                setTimeout(function() { throw e }, 0);
              }
            }
          }
        }, Math.round(next));
      }
      queue$1.push({
        handle: ++id$1,
        callback: callback,
        cancelled: false
      });
      return id$1
    };

    caf = function(handle) {
      for(var i = 0; i < queue$1.length; i++) {
        if(queue$1[i].handle === handle) {
          queue$1[i].cancelled = true;
        }
      }
    };
  }

  var raf_1 = function(fn) {
    // Wrap in a new function to prevent
    // `cancel` potentially being assigned
    // to the native rAF function
    return raf.call(root, fn)
  };
  var cancel = function() {
    caf.apply(root, arguments);
  };
  var polyfill = function(object) {
    if (!object) {
      object = root;
    }
    object.requestAnimationFrame = raf;
    object.cancelAnimationFrame = caf;
  };
  raf_1.cancel = cancel;
  raf_1.polyfill = polyfill;

  function createStore(initialState) {
    var state = initialState;

    var addActor = function addActor(actor) {
      return function (payload) {
        /* istanbul ignore next */
        if (process.env.NODE_ENV === 'development') {
          console.log('Updating state using %o and payload %o', actor.name, payload);
        }

        var nextPartialState = actor(state, payload);
        var nextState = nextPartialState ? _objectSpread({}, state, copyDefinedValues(nextPartialState)) : state;
        notify(nextState);
      };
    };

    var animationFrameId = null;
    var listeners = [];

    var notify = function notify(nextState) {
      if (state === nextState) {
        return;
      }

      state = nextState;

      if (animationFrameId) {
        raf_1.cancel(animationFrameId);
      }

      animationFrameId = raf_1(function () {
        return listeners.forEach(function (listener) {
          return listener();
        });
      });
    };

    var subscribe = function subscribe(listener) {
      listeners.push(listener);
      return function () {
        var indexOfListener = listeners.indexOf(listener);

        if (indexOfListener > -1) {
          listeners.splice(indexOfListener, 1);
        }
      };
    };

    return {
      addActor: addActor,
      subscribe: subscribe,
      getState: function getState() {
        return state;
      }
    };
  }

  var setFirstDayOfWeekActor = function setFirstDayOfWeekActor(state, payload) {
    if (typeof payload.value === 'number' && payload.value >= 0 && payload.value <= 6) {
      return {
        firstDayOfWeek: payload.value
      };
    }
  };

  var getFromDate = function getFromDate(dateObject) {
    return {
      hours: dateObject.getHours(),
      minutes: dateObject.getMinutes()
    };
  };

  var getHighlighted = function getHighlighted(selected, view) {
    if (selected && isSameMonth(selected, view)) {
      return new Date(selected.getFullYear(), selected.getMonth(), selected.getDate());
    }

    return new Date(view.getFullYear(), view.getMonth(), 1);
  };

  var setSelectedActor = function setSelectedActor(state, payload) {
    var selected = getNextSelected(state, payload);

    if (!selected || isDisabled(state, selected)) {
      return;
    }

    if (state.selected) {
      var time = getFromDate(state.selected);
      selected.setHours(time.hours, time.minutes);
    }

    var view = getNextView(state, payload, selected);
    return {
      selected: selected,
      highlighted: getNextHighlighted(state, payload, selected, view),
      view: view
    };
  };

  var getNextSelected = function getNextSelected(state, payload) {
    if (payload.value instanceof Date) {
      return new Date(payload.value);
    }

    if (Number.isInteger(payload.value)) {
      return new Date(payload.value);
    }
  };

  var getNextView = function getNextView(state, payload, nextSelected) {
    if (payload.isUpdatingView === false || isSameMonth(state.view, nextSelected)) {
      return;
    }

    return getStartDateOfMonth(nextSelected);
  };

  var getNextHighlighted = function getNextHighlighted(state, payload, nextSelected, nextView) {
    if (payload.isUpdatingView === false) {
      return;
    }

    return getHighlighted(nextSelected, nextView || state.view);
  };

  var setViewActor = function setViewActor(state, payload) {
    if (payload.value instanceof Date) {
      return {
        highlighted: getHighlighted(state.selected, payload.value),
        view: getStartDateOfMonth(payload.value)
      };
    }
  };

  var setMinimumActor = function setMinimumActor(state, payload) {
    if (payload.value instanceof Date) {
      return {
        minimum: payload.value
      };
    }
  };

  var setMaximumActor = function setMaximumActor(state, payload) {
    if (payload.value instanceof Date) {
      return {
        maximum: payload.value
      };
    }
  };

  var setDisabledActor = function setDisabledActor(state, payload) {
    if (!Array.isArray(payload.value)) {
      return;
    }

    var disabled = payload.value.filter(function (value) {
      // Keep in disabled weekdays
      if (typeof value === 'number') {
        return value >= 0 && value <= 6;
      } // Keep in disabled range of dates


      if (Array.isArray(value)) {
        return value.length === 2 && value[0] instanceof Date && value[1] instanceof Date;
      } // Keep in disabled individual dates


      return value instanceof Date;
    });
    return {
      disabled: disabled
    };
  };

  var setTemplateActor = function setTemplateActor(state, payload) {
    if (payload.value && typeof payload.value === 'string') {
      return {
        template: payload.value
      };
    }
  };

  var setTemplateHookWordsActor = function setTemplateHookWordsActor(state, payload) {
    var MMM = getValidMonths(payload.MMM, state.templateHookWords.MMM);
    var MMMM = getValidMonths(payload.MMMM, state.templateHookWords.MMMM);
    var DDD = getValidDays(payload.DDD, state.templateHookWords.DDD);
    var DDDD = getValidDays(payload.DDDD, state.templateHookWords.DDDD);
    return {
      templateHookWords: {
        MMM: MMM,
        MMMM: MMMM,
        DDD: DDD,
        DDDD: DDDD
      }
    };
  };

  var getValidMonths = function getValidMonths(payloadMonths, stateMonths) {
    if (Array.isArray(payloadMonths) && payloadMonths.length === 12 && payloadMonths.every(function (month) {
      return typeof month === 'string';
    })) {
      return payloadMonths;
    }

    return stateMonths;
  };

  var getValidDays = function getValidDays(payloadDays, stateDays) {
    if (Array.isArray(payloadDays) && payloadDays.length === 7 && payloadDays.every(function (day) {
      return typeof day === 'string';
    })) {
      return payloadDays;
    }

    return stateDays;
  };

  var setStateActor = function setStateActor(state, payload) {
    if (payload.firstDayOfWeek) {
      state = _objectSpread({}, state, setFirstDayOfWeekActor(state, {
        value: payload.firstDayOfWeek
      }));
    }

    if (payload.selected) {
      state = _objectSpread({}, state, setSelectedActor(state, {
        value: payload.selected
      }));
    }

    if (payload.view) {
      state = _objectSpread({}, state, setViewActor(state, {
        value: payload.view
      }));
    }

    if (payload.minimum) {
      state = _objectSpread({}, state, setMinimumActor(state, {
        value: payload.minimum
      }));
    }

    if (payload.maximum) {
      state = _objectSpread({}, state, setMaximumActor(state, {
        value: payload.maximum
      }));
    }

    if (payload.disabled) {
      state = _objectSpread({}, state, setDisabledActor(state, {
        value: payload.disabled
      }));
    }

    if (payload.template) {
      state = _objectSpread({}, state, setTemplateActor(state, {
        value: payload.template
      }));
    }

    if (payload.templateHookWords) {
      state = _objectSpread({}, state, setTemplateHookWordsActor(state, payload.templateHookWords));
    }

    return state;
  };

  var _KEY_CODE_TO_DAY_DELT;
  var KEY_CODE_TO_DAY_DELTA = (_KEY_CODE_TO_DAY_DELT = {}, _defineProperty(_KEY_CODE_TO_DAY_DELT, KEY_CODE.DOWN, 7), _defineProperty(_KEY_CODE_TO_DAY_DELT, KEY_CODE.UP, -7), _defineProperty(_KEY_CODE_TO_DAY_DELT, KEY_CODE.LEFT, -1), _defineProperty(_KEY_CODE_TO_DAY_DELT, KEY_CODE.RIGHT, 1), _KEY_CODE_TO_DAY_DELT);

  var setHighlightedActor = function setHighlightedActor(state, payload) {
    var dayDelta = KEY_CODE_TO_DAY_DELTA[payload.keyCode];

    if (!dayDelta) {
      return;
    }

    var highlighted = new Date(state.highlighted);
    highlighted.setDate(highlighted.getDate() + dayDelta);
    var view = isSameMonth(state.view, highlighted) ? undefined : getStartDateOfMonth(highlighted);
    return {
      highlighted: highlighted,
      view: view
    };
  };

  var setTimeActor = function setTimeActor(state, payload) {
    var selected = state.selected;

    if (!selected) {
      throw new Error('Cannot set the time without a selected date');
    }

    if (payload.hours != null && payload.hours !== selected.getHours()) {
      selected = new Date(selected);
      selected.setHours(payload.hours);
    }

    if (payload.minutes != null && payload.minutes !== selected.getMinutes()) {
      selected = new Date(selected);
      selected.setMinutes(payload.minutes);
    }

    if (selected !== state.selected) {
      return {
        selected: selected
      };
    }
  };

  var clearActor = function clearActor(state, payload) {
    return {
      selected: null
    };
  };

  var viewPreviousActor = function viewPreviousActor(state) {
    var view = state.view;
    view = new Date(view.getFullYear(), view.getMonth() - 1, 1);
    return {
      highlighted: getHighlighted(state.selected, view),
      view: view
    };
  };

  var viewNextActor = function viewNextActor(state) {
    var view = state.view;
    view = new Date(view.getFullYear(), view.getMonth() + 1, 1);
    return {
      highlighted: getHighlighted(state.selected, view),
      view: view
    };
  };

  var viewTodayActor = function viewTodayActor(state) {
    var view = getStartDateOfMonth(new Date());
    return {
      highlighted: getHighlighted(state.selected, view),
      view: view
    };
  };

  var createCycleHourActor = function createCycleHourActor(getNextHours) {
    return function cycleHourActor(state) {
      var selected = state.selected;

      if (!selected) {
        throw new Error('Cannot cycle the hour without a selected date');
      }

      var hoursInMeridiem = getNextHours(selected.getHours() % 12);
      var isPostMeridiem = selected.getHours() > 11;

      if (isPostMeridiem) {
        hoursInMeridiem += 12;
      }

      selected = new Date(selected);
      selected.setHours(hoursInMeridiem);
      return {
        selected: selected
      };
    };
  };

  var cycleHourUpActor = createCycleHourActor(function (hoursInMeridiem) {
    return hoursInMeridiem === 11 ? 0 : hoursInMeridiem + 1;
  });

  var cycleHourDownActor = createCycleHourActor(function (hoursInMeridiem) {
    return hoursInMeridiem === 0 ? 11 : hoursInMeridiem - 1;
  });

  var createCycleMinuteActor = function createCycleMinuteActor(getNextMinutes) {
    return function cycleMinuteActor(state) {
      var selected = state.selected;

      if (!selected) {
        throw new Error('Cannot cycle the minute without a selected date');
      }

      var minutes = getNextMinutes(selected.getMinutes(), 15);
      selected = new Date(selected);
      selected.setMinutes(minutes);
      return {
        selected: selected
      };
    };
  };

  var cycleMinuteUpActor = createCycleMinuteActor(function (minutes, interval) {
    return minutes + interval >= 60 ? 0 : minutes + interval;
  });

  var cycleMinuteDownActor = createCycleMinuteActor(function (minutes, interval) {
    return minutes - interval < 0 ? 60 - interval : minutes - interval;
  });

  var cycleMeridiemActor = function cycleMeridiemActor(state) {
    var selected = state.selected;

    if (!selected) {
      throw new Error('Cannot cycle the meridiem without a selected date');
    }

    var hours = selected.getHours();
    hours += hours <= 11 ? 12 : -12;
    selected = new Date(selected);
    selected.setHours(hours);
    return {
      selected: selected
    };
  };

  var getTranslatedPartialState = function getTranslatedPartialState(partialState, partialTranslation) {
    if (!partialTranslation) {
      return partialState;
    }

    return _objectSpread({}, partialState, {
      firstDayOfWeek: partialState.firstDayOfWeek != null ? partialState.firstDayOfWeek : partialTranslation.firstDayOfWeek,
      template: partialState.template != null ? partialState.template : partialTranslation.template,
      templateHookWords: _objectSpread({}, partialTranslation.templateHookWords, partialState.templateHookWords)
    });
  };

  var getInitialState = function getInitialState() {
    var view = getStartDateOfMonth(new Date());
    return {
      firstDayOfWeek: 0,
      selected: null,
      highlighted: view,
      view: view,
      minimum: null,
      maximum: null,
      disabled: [],
      template: 'D MMMM, YYYY',
      templateHookWords: TEMPLATE_HOOK_WORDS
    };
  };

  var createGetValue = function createGetValue(store) {
    return function (customTemplate) {
      var _store$getState = store.getState(),
          selected = _store$getState.selected,
          template = _store$getState.template,
          templateHookWords = _store$getState.templateHookWords;

      return selected ? format(selected, customTemplate || template, templateHookWords) : '';
    };
  };

  var createSubscribeToSelection = function createSubscribeToSelection(store, getValue) {
    var currentSelected = store.getState().selected;
    return function (listener) {
      return store.subscribe(function () {
        var selected = store.getState().selected;

        if (currentSelected === selected || currentSelected && selected && currentSelected.getTime() === selected.getTime()) {
          return;
        }

        var value = getValue();
        listener({
          value: value,
          date: selected
        });
        currentSelected = selected;
      });
    };
  };

  function createPicker() {
    var partialState = arguments.length > 0 && arguments[0] !== undefined ? arguments[0] : {};
    var partialTranslation = arguments.length > 1 ? arguments[1] : undefined;
    var initialState = getInitialState();
    var store = createStore(initialState);
    var setState = store.addActor(setStateActor);
    setState(getTranslatedPartialState(partialState, partialTranslation));
    var getValue = createGetValue(store);
    var subscribeToSelection = createSubscribeToSelection(store, getValue);
    return {
      store: store,
      getValue: getValue,
      subscribeToSelection: subscribeToSelection,
      setState: setState,
      setFirstDayOfWeek: store.addActor(setFirstDayOfWeekActor),
      setSelected: store.addActor(setSelectedActor),
      setView: store.addActor(setViewActor),
      setHighlighted: store.addActor(setHighlightedActor),
      setMinimum: store.addActor(setMinimumActor),
      setMaximum: store.addActor(setMaximumActor),
      setDisabled: store.addActor(setDisabledActor),
      setTime: store.addActor(setTimeActor),
      setTemplate: store.addActor(setTemplateActor),
      setTemplateHookWords: store.addActor(setTemplateHookWordsActor),
      clear: store.addActor(clearActor),
      viewPrevious: store.addActor(viewPreviousActor),
      viewNext: store.addActor(viewNextActor),
      viewToday: store.addActor(viewTodayActor),
      cycleHourUp: store.addActor(cycleHourUpActor),
      cycleHourDown: store.addActor(cycleHourDownActor),
      cycleMinuteUp: store.addActor(cycleMinuteUpActor),
      cycleMinuteDown: store.addActor(cycleMinuteDownActor),
      cycleMeridiem: store.addActor(cycleMeridiemActor)
    };
  }

  var chevronLeft = "<svg viewBox=\"0 0 32 54\"><path d=\"M25.04 3L1 27.01l24.04 24.01 5-4.989L11 27.01 30.04 7.99z\"/></svg>";

  var chevronRight = "<svg viewBox=\"0 0 32 54\"><path d=\"M7 51.02l24.04-24.01L7 3 2 7.989 21.04 27.01 2 46.03z\"/></svg>";

  var chevronUp = "<svg viewBox=\"0 0 54 32\"><path d=\"M50.53 25.53L26.52 1.49 2.51 25.53l4.989 5L26.52 11.49l19.02 19.04z\"/></svg>";

  var chevronDown = "<svg viewBox=\"0 0 54 32\"><path d=\"M2.51 7.49l24.01 24.04L50.53 7.49l-4.989-5L26.52 21.53 7.5 2.49z\"/></svg>";

  var bullsEye = "<svg viewBox=\"0 0 54 54\"><path d=\"M27 46C16.51 46 8 37.49 8 27S16.51 8 27 8s19 8.51 19 19-8.51 19-19 19m0-45C12.64 1 1 12.64 1 27s11.64 26 26 26 26-11.64 26-26S41.36 1 27 1\"/><path d=\"M31 27c0 2.21-1.79 4-4 4s-4-1.79-4-4 1.79-4 4-4 4 1.79 4 4\"/></svg>";

  var DatePickerContext = React.createContext(RENDER_OPTIONS);
  var PickadateContext = React.createContext(null);
  var InputPickerContext = React.createContext(null);

  var getClassName = function getClassName(_ref) {
    var styleName = _ref.styleName,
        options = _ref.options;
    var styleList = Array.isArray(styleName) ? styleName : [styleName];
    var classList = styleList.map(function (style) {
      return style ? join(options.className[style]) : '';
    });
    classList.unshift('pickadate--element');
    return classList.join(' ');
  };

  var getDataAttributes = function getDataAttributes(_ref2) {
    var dataset = _ref2.dataset;

    if (!dataset) {
      return;
    }

    return Object.keys(dataset).reduce(function (accumulator, key) {
      accumulator["data-".concat(key)] = dataset[key];
      return accumulator;
    }, {});
  };

  var getEventTarget = function getEventTarget(event) {
    var currentTarget = event.target;

    while (currentTarget.parentNode) {
      if (currentTarget instanceof HTMLInputElement || currentTarget instanceof HTMLButtonElement) {
        return currentTarget;
      }

      currentTarget = currentTarget.parentNode;
    }

    return currentTarget;
  };

  var ViewComponent = function ViewComponent(_ref3) {
    var children = _ref3.children,
        styleName = _ref3.styleName,
        isHidden = _ref3.isHidden,
        dataset = _ref3.dataset;
    return React.createElement(DatePickerContext.Consumer, null, function (options) {
      return React.createElement("div", _extends({}, getDataAttributes({
        dataset: dataset
      }), {
        className: getClassName({
          styleName: styleName,
          options: options
        }),
        hidden: isHidden
      }), children);
    });
  };

  var ButtonComponent = function ButtonComponent(_ref4) {
    var children = _ref4.children,
        styleName = _ref4.styleName,
        onPress = _ref4.onPress,
        onKeyDown = _ref4.onKeyDown,
        tabIndex = _ref4.tabIndex;
    return React.createElement(DatePickerContext.Consumer, null, function (options) {
      return React.createElement(InputPickerContext.Consumer, null, function (isInputOpen) {
        return React.createElement("button", {
          className: getClassName({
            styleName: styleName,
            options: options
          }),
          onClick: onPress,
          onKeyDown: onKeyDown,
          disabled: isInputOpen === false,
          tabIndex: tabIndex,
          type: "button"
        }, children);
      });
    });
  };

  var InputComponent = function InputComponent(_ref5) {
    var styleName = _ref5.styleName,
        value = _ref5.value,
        readOnly = _ref5.readOnly;
    return React.createElement(DatePickerContext.Consumer, null, function (options) {
      return React.createElement("input", {
        className: getClassName({
          styleName: styleName,
          options: options
        }),
        value: value,
        readOnly: readOnly
      });
    });
  };

  setComponents({
    ButtonComponent: ButtonComponent,
    ViewComponent: ViewComponent,
    InputComponent: InputComponent
  }); /////////////////
  // DATE PICKER //
  /////////////////

  var DatePicker = function DatePicker(props) {
    return React.createElement(PickadateContext.Consumer, null, function (picker) {
      return picker ? React.createElement(DatePickerWithApi, _extends({}, props, {
        picker: picker
      })) : React.createElement(DatePickerWithoutApi, props);
    });
  };

  var DatePickerWithoutApi =
  /*#__PURE__*/
  function (_React$Component) {
    _inherits(DatePickerWithoutApi, _React$Component);

    function DatePickerWithoutApi() {
      var _getPrototypeOf2;

      var _this;

      _classCallCheck(this, DatePickerWithoutApi);

      for (var _len = arguments.length, args = new Array(_len), _key = 0; _key < _len; _key++) {
        args[_key] = arguments[_key];
      }

      _this = _possibleConstructorReturn(this, (_getPrototypeOf2 = _getPrototypeOf(DatePickerWithoutApi)).call.apply(_getPrototypeOf2, [this].concat(args)));
      _this.picker = createPicker(_this.props.initialState, _this.props.initialTranslation);
      return _this;
    }

    _createClass(DatePickerWithoutApi, [{
      key: "render",
      value: function render() {
        var _this$props = this.props,
            initialState = _this$props.initialState,
            rest = _objectWithoutProperties(_this$props, ["initialState"]);

        return React.createElement(DatePickerWithApi, _extends({}, rest, {
          picker: this.picker
        }));
      }
    }]);

    return DatePickerWithoutApi;
  }(React.Component);

  var DatePickerWithApi =
  /*#__PURE__*/
  function (_React$Component2) {
    _inherits(DatePickerWithApi, _React$Component2);

    function DatePickerWithApi() {
      var _getPrototypeOf3;

      var _this2;

      _classCallCheck(this, DatePickerWithApi);

      for (var _len2 = arguments.length, args = new Array(_len2), _key2 = 0; _key2 < _len2; _key2++) {
        args[_key2] = arguments[_key2];
      }

      _this2 = _possibleConstructorReturn(this, (_getPrototypeOf3 = _getPrototypeOf(DatePickerWithApi)).call.apply(_getPrototypeOf3, [this].concat(args)));
      _this2.unsubscribe = _this2.props.picker.store.subscribe(function () {
        return _this2.forceUpdate();
      });
      _this2.unsubscribeSelection = _this2.props.picker.subscribeToSelection(function (_ref6) {
        var value = _ref6.value,
            date = _ref6.date;
        var onChangeValue = _this2.props.onChangeValue;

        if (onChangeValue) {
          onChangeValue({
            value: value,
            date: date
          });
        }
      });
      return _this2;
    }

    _createClass(DatePickerWithApi, [{
      key: "componentWillUnmount",
      value: function componentWillUnmount() {
        this.unsubscribe();
        this.unsubscribeSelection();
      }
    }, {
      key: "render",
      value: function render() {
        var _this$props2 = this.props,
            picker = _this$props2.picker,
            _this$props2$isInputA = _this$props2.isInputActive,
            isInputActive = _this$props2$isInputA === void 0 ? false : _this$props2$isInputA,
            _this$props2$isInputO = _this$props2.isInputOpen,
            isInputOpen = _this$props2$isInputO === void 0 ? false : _this$props2$isInputO;
        var options = this.props.options;
        options = options ? mergeUpdates(RENDER_OPTIONS, options) : RENDER_OPTIONS;

        var onPressGrid = function onPressGrid(event) {
          var value = getValueFromMouseEvent(event);

          if (value) {
            picker.setSelected({
              value: value
            });
          }
        };

        return React.createElement(DatePickerContext.Provider, {
          value: options
        }, React.createElement(RootBox, {
          isOpenForInput: isInputOpen
        }, React.createElement(HeaderBox, null, React.createElement(MonthAndYearBox, {
          picker: picker
        }), React.createElement(PreviousButton, {
          picker: picker,
          renderIcon: function renderIcon() {
            return React.createElement("div", {
              dangerouslySetInnerHTML: {
                __html: chevronLeft
              }
            });
          }
        }), React.createElement(TodayButton, {
          picker: picker,
          renderIcon: function renderIcon() {
            return React.createElement("div", {
              dangerouslySetInnerHTML: {
                __html: bullsEye
              }
            });
          }
        }), React.createElement(NextButton, {
          picker: picker,
          renderIcon: function renderIcon() {
            return React.createElement("div", {
              dangerouslySetInnerHTML: {
                __html: chevronRight
              }
            });
          }
        })), React.createElement(BodyBox, null, React.createElement(GridButton, {
          picker: picker,
          onPress: onPressGrid,
          renderCell: options.renderCell,
          isRemoteActive: isInputActive
        })), this.renderFooterBox({
          options: options
        })));
      }
    }, {
      key: "renderFooterBox",
      value: function renderFooterBox(_ref7) {
        var options = _ref7.options;

        if (options.mode !== 'date-time') {
          return;
        }

        var picker = this.props.picker;
        return React.createElement(FooterBox, null, React.createElement(TimeBox, {
          picker: picker
        }, React.createElement(HourInput, {
          picker: picker,
          renderIconUp: function renderIconUp() {
            return React.createElement("div", {
              dangerouslySetInnerHTML: {
                __html: chevronUp
              }
            });
          },
          renderIconDown: function renderIconDown() {
            return React.createElement("div", {
              dangerouslySetInnerHTML: {
                __html: chevronDown
              }
            });
          }
        }), React.createElement(TimeSeparator, null), React.createElement(MinuteInput, {
          picker: picker,
          renderIconUp: function renderIconUp() {
            return React.createElement("div", {
              dangerouslySetInnerHTML: {
                __html: chevronUp
              }
            });
          },
          renderIconDown: function renderIconDown() {
            return React.createElement("div", {
              dangerouslySetInnerHTML: {
                __html: chevronDown
              }
            });
          }
        }), React.createElement(MeridiemButton, {
          picker: picker
        })));
      }
    }]);

    return DatePickerWithApi;
  }(React.Component); ///////////////
  // DATE TEXT //
  ///////////////


  var DateText = function DateText(props) {
    return React.createElement(PickadateContext.Consumer, null, function (picker) {
      if (!picker) {
        console.error('Cannot render the date text outside a <Pickadate> component');
        return null;
      }

      return React.createElement(DateTextWithApi, _extends({}, props, {
        picker: picker
      }));
    });
  };

  var DateTextWithApi =
  /*#__PURE__*/
  function (_React$Component3) {
    _inherits(DateTextWithApi, _React$Component3);

    function DateTextWithApi() {
      var _getPrototypeOf4;

      var _this3;

      _classCallCheck(this, DateTextWithApi);

      for (var _len3 = arguments.length, args = new Array(_len3), _key3 = 0; _key3 < _len3; _key3++) {
        args[_key3] = arguments[_key3];
      }

      _this3 = _possibleConstructorReturn(this, (_getPrototypeOf4 = _getPrototypeOf(DateTextWithApi)).call.apply(_getPrototypeOf4, [this].concat(args)));
      _this3.unsubscribe = null;
      return _this3;
    }

    _createClass(DateTextWithApi, [{
      key: "componentDidMount",
      value: function componentDidMount() {
        var _this4 = this;

        this.unsubscribe = this.props.picker.store.subscribe(function () {
          return _this4.forceUpdate();
        });
      }
    }, {
      key: "componentWillUnmount",
      value: function componentWillUnmount() {
        if (this.unsubscribe) {
          this.unsubscribe();
        }
      }
    }, {
      key: "render",
      value: function render() {
        var _this$props3 = this.props,
            picker = _this$props3.picker,
            renderFallback = _this$props3.renderFallback;
        return React.createElement("div", null, picker.getValue() || renderFallback());
      }
    }]);

    return DateTextWithApi;
  }(React.Component); ///////////
  // INPUT //
  ///////////


  var Input$1 = function Input(props) {
    return React.createElement(PickadateContext.Consumer, null, function (picker) {
      if (!picker) {
        console.error('Cannot render the date input outside a <Pickadate> component');
        return null;
      }

      return React.createElement(InputWithApi, _extends({}, props, {
        picker: picker
      }));
    });
  };

  var InputWithApi =
  /*#__PURE__*/
  function (_React$Component4) {
    _inherits(InputWithApi, _React$Component4);

    function InputWithApi() {
      var _getPrototypeOf5;

      var _this5;

      _classCallCheck(this, InputWithApi);

      for (var _len4 = arguments.length, args = new Array(_len4), _key4 = 0; _key4 < _len4; _key4++) {
        args[_key4] = arguments[_key4];
      }

      _this5 = _possibleConstructorReturn(this, (_getPrototypeOf5 = _getPrototypeOf(InputWithApi)).call.apply(_getPrototypeOf5, [this].concat(args)));
      _this5.unsubscribe = _this5.props.picker.store.subscribe(function () {
        return _this5.forceUpdate();
      });
      _this5.unsubscribeSelection = _this5.props.picker.subscribeToSelection(function (_ref8) {
        var value = _ref8.value,
            date = _ref8.date;
        var onChangeValue = _this5.props.onChangeValue;

        if (onChangeValue) {
          onChangeValue({
            value: value,
            date: date
          });
        }
      });
      return _this5;
    }

    _createClass(InputWithApi, [{
      key: "componentWillUnmount",
      value: function componentWillUnmount() {
        this.unsubscribe();
        this.unsubscribeSelection();
      }
    }, {
      key: "render",
      value: function render() {
        var _this$props4 = this.props,
            picker = _this$props4.picker,
            inputRef = _this$props4.inputRef,
            onChangeValue = _this$props4.onChangeValue,
            rest = _objectWithoutProperties(_this$props4, ["picker", "inputRef", "onChangeValue"]);

        return React.createElement("input", _extends({
          readOnly: true
        }, rest, {
          ref: inputRef,
          value: picker.getValue()
        }));
      }
    }]);

    return InputWithApi;
  }(React.Component); //////////////////
  // INPUT PICKER //
  //////////////////


  var InputPicker = function InputPicker(props) {
    return React.createElement(PickadateContext.Consumer, null, function (picker) {
      if (picker) {
        console.error('Cannot render the input picker within a <Pickadate> component');
        return null;
      }

      return React.createElement(InputPickerWithoutApi, props);
    });
  };

  var InputPickerWithoutApi =
  /*#__PURE__*/
  function (_React$Component5) {
    _inherits(InputPickerWithoutApi, _React$Component5);

    function InputPickerWithoutApi() {
      var _getPrototypeOf6;

      var _this6;

      _classCallCheck(this, InputPickerWithoutApi);

      for (var _len5 = arguments.length, args = new Array(_len5), _key5 = 0; _key5 < _len5; _key5++) {
        args[_key5] = arguments[_key5];
      }

      _this6 = _possibleConstructorReturn(this, (_getPrototypeOf6 = _getPrototypeOf(InputPickerWithoutApi)).call.apply(_getPrototypeOf6, [this].concat(args)));
      _this6.state = {
        isInputActive: false,
        isInputOpen: false
      };
      _this6.inputRef = React.createRef();
      _this6.viewRef = React.createRef();
      _this6.picker = createPicker(_this6.props.initialState, _this6.props.initialTranslation);
      _this6.unsubscribeSelection = _this6.picker.subscribeToSelection(function (_ref9) {
        var value = _ref9.value,
            date = _ref9.date;
        var onChangeValue = _this6.props.onChangeValue;

        if (onChangeValue) {
          onChangeValue({
            value: value,
            date: date
          });
        }
      });
      _this6.onKeyDown = createOnKeyDownCalendar(_this6.picker);

      _this6.onDocumentEvent = function (event) {
        var inputElement = _this6.inputRef.current;
        var wrapperElement = _this6.viewRef.current;

        if (!inputElement || !wrapperElement) {
          console.error('Cannot handle document events without a mounted input picker');
          return;
        }

        if (event.target !== inputElement && !wrapperElement.contains(event.target) && // $FlowFixMe: In Chrome 62+, password auto-fill simulates focusin
        !event.isSimulated && // In Firefox, a click on an `option` element bubbles up directly
        // to the document.
        event.target !== document && // In Firefox stopPropagation() doesn’t prevent right-click events
        // from bubbling. So make sure the event wasn’t a right-click.
        // $FlowFixMe
        event.which !== 3) {
          _this6.setState({
            isInputOpen: false
          });
        }
      };

      return _this6;
    }

    _createClass(InputPickerWithoutApi, [{
      key: "componentDidMount",
      value: function componentDidMount() {
        document.addEventListener('focusin', this.onDocumentEvent);
        document.addEventListener('click', this.onDocumentEvent);
      }
    }, {
      key: "componentWillUnmount",
      value: function componentWillUnmount() {
        this.unsubscribeSelection();
        document.removeEventListener('focusin', this.onDocumentEvent);
        document.removeEventListener('click', this.onDocumentEvent);
      }
    }, {
      key: "render",
      value: function render() {
        var _this7 = this;

        var _this$props5 = this.props,
            initialState = _this$props5.initialState,
            initialTranslation = _this$props5.initialTranslation,
            options = _this$props5.options,
            propsOnFocus = _this$props5.onFocus,
            propsOnBlur = _this$props5.onBlur,
            propsOnKeyDown = _this$props5.onKeyDown,
            rest = _objectWithoutProperties(_this$props5, ["initialState", "initialTranslation", "options", "onFocus", "onBlur", "onKeyDown"]);

        var onFocus = function onFocus(event) {
          _this7.setState({
            isInputActive: true,
            isInputOpen: true
          });

          if (propsOnFocus) {
            propsOnFocus(event);
          }
        };

        var onBlur = function onBlur(event) {
          _this7.setState({
            isInputActive: false
          });

          if (propsOnBlur) {
            propsOnBlur(event);
          }
        };

        var onKeyDown = function onKeyDown(event) {
          _this7.onKeyDown(event);

          if (propsOnKeyDown) {
            propsOnKeyDown(event);
          }
        };

        var onMouseDown = function onMouseDown(event) {
          var target = getEventTarget(event);

          if (target instanceof HTMLInputElement || target instanceof HTMLButtonElement) {
            return;
          }

          event.preventDefault();
        };

        var picker = this.picker;
        var _this$state = this.state,
            isInputActive = _this$state.isInputActive,
            isInputOpen = _this$state.isInputOpen;
        return React.createElement(PickadateContext.Provider, {
          value: picker
        }, React.createElement(Input$1, _extends({}, rest, {
          onFocus: onFocus,
          onBlur: onBlur,
          onKeyDown: onKeyDown,
          inputRef: this.inputRef
        })), React.createElement(InputPickerContext.Provider, {
          value: isInputOpen
        }, React.createElement(ViewComponent, {
          styleName: "inputRoot"
        }, React.createElement("div", {
          ref: this.viewRef,
          onMouseDown: onMouseDown
        }, React.createElement(DatePicker, {
          options: options,
          isInputActive: isInputActive,
          isInputOpen: isInputOpen
        })))));
      }
    }]);

    return InputPickerWithoutApi;
  }(React.Component); ///////////////
  // PICKADATE //
  ///////////////


  var Pickadate =
  /*#__PURE__*/
  function (_React$Component6) {
    _inherits(Pickadate, _React$Component6);

    function Pickadate() {
      var _getPrototypeOf7;

      var _this8;

      _classCallCheck(this, Pickadate);

      for (var _len6 = arguments.length, args = new Array(_len6), _key6 = 0; _key6 < _len6; _key6++) {
        args[_key6] = arguments[_key6];
      }

      _this8 = _possibleConstructorReturn(this, (_getPrototypeOf7 = _getPrototypeOf(Pickadate)).call.apply(_getPrototypeOf7, [this].concat(args)));
      _this8.picker = createPicker(_this8.props.initialState, _this8.props.initialTranslation);
      _this8.unsubscribe = _this8.picker.subscribeToSelection(function (_ref10) {
        var value = _ref10.value,
            date = _ref10.date;
        var onChangeValue = _this8.props.onChangeValue;

        if (onChangeValue) {
          onChangeValue({
            value: value,
            date: date
          });
        }
      });
      return _this8;
    }

    _createClass(Pickadate, [{
      key: "componentWillUnmount",
      value: function componentWillUnmount() {
        this.unsubscribe();
      }
    }, {
      key: "render",
      value: function render() {
        var children = this.props.children;
        return React.createElement(PickadateContext.Provider, {
          value: this.picker
        }, children);
      }
    }]);

    return Pickadate;
  }(React.Component);

  Pickadate.DatePicker = DatePicker;
  Pickadate.DateText = DateText;
  Pickadate.Input = Input$1;
  Pickadate.InputPicker = InputPicker;

  return Pickadate;

}));

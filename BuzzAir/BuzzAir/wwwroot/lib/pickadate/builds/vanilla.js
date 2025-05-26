/* pickadate v5.0.0-alpha.3, @license MIT */
(function (global, factory) {
  typeof exports === 'object' && typeof module !== 'undefined' ? module.exports = factory() :
  typeof define === 'function' && define.amd ? define(factory) :
  (global = global || self, global.pickadate = factory());
}(this, function () { 'use strict';

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

  function _slicedToArray(arr, i) {
    return _arrayWithHoles(arr) || _iterableToArrayLimit(arr, i) || _nonIterableRest();
  }

  function _toConsumableArray(arr) {
    return _arrayWithoutHoles(arr) || _iterableToArray(arr) || _nonIterableSpread();
  }

  function _arrayWithoutHoles(arr) {
    if (Array.isArray(arr)) {
      for (var i = 0, arr2 = new Array(arr.length); i < arr.length; i++) arr2[i] = arr[i];

      return arr2;
    }
  }

  function _arrayWithHoles(arr) {
    if (Array.isArray(arr)) return arr;
  }

  function _iterableToArray(iter) {
    if (Symbol.iterator in Object(iter) || Object.prototype.toString.call(iter) === "[object Arguments]") return Array.from(iter);
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

  function _nonIterableSpread() {
    throw new TypeError("Invalid attempt to spread non-iterable instance");
  }

  function _nonIterableRest() {
    throw new TypeError("Invalid attempt to destructure non-iterable instance");
  }

  /////////////
  // STRINGS //
  /////////////
  var caseDash = function caseDash(string) {
    return string.split(/(?=[A-Z])/g).map(function (chunk) {
      return chunk.toLowerCase();
    }).join('-');
  }; /////////////
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

  // prettier-ignore
  var EVENT_NAME = {
    CHANGE: 'pickadate:change',
    MOUNT: 'pickadate:mount',
    UNMOUNT: 'pickadate:unmount',
    INPUT_ACTIVE: 'pickadate:input-active',
    INPUT_INACTIVE: 'pickadate:input-inactive',
    INPUT_OPEN: 'pickadate:input-open',
    INPUT_CLOSE: 'pickadate:input-close'
  };

  var chevronRight = "<svg viewBox=\"0 0 32 54\"><path d=\"M7 51.02l24.04-24.01L7 3 2 7.989 21.04 27.01 2 46.03z\"/></svg>";

  var chevronLeft = "<svg viewBox=\"0 0 32 54\"><path d=\"M25.04 3L1 27.01l24.04 24.01 5-4.989L11 27.01 30.04 7.99z\"/></svg>";

  var chevronUp = "<svg viewBox=\"0 0 54 32\"><path d=\"M50.53 25.53L26.52 1.49 2.51 25.53l4.989 5L26.52 11.49l19.02 19.04z\"/></svg>";

  var chevronDown = "<svg viewBox=\"0 0 54 32\"><path d=\"M2.51 7.49l24.01 24.04L50.53 7.49l-4.989-5L26.52 21.53 7.5 2.49z\"/></svg>";

  var bullsEye = "<svg viewBox=\"0 0 54 54\"><path d=\"M27 46C16.51 46 8 37.49 8 27S16.51 8 27 8s19 8.51 19 19-8.51 19-19 19m0-45C12.64 1 1 12.64 1 27s11.64 26 26 26 26-11.64 26-26S41.36 1 27 1\"/><path d=\"M31 27c0 2.21-1.79 4-4 4s-4-1.79-4-4 1.79-4 4-4 4 1.79 4 4\"/></svg>";

  // LAYOUTS //
  /////////////

  var createLayout = function createLayout(createElement) {
    return function (initialize) {
      return function () {
        for (var _len = arguments.length, childLayouts = new Array(_len), _key = 0; _key < _len; _key++) {
          childLayouts[_key] = arguments[_key];
        }

        return function (picker, options, rootElement) {
          var element = createElement(picker, rootElement);
          initialize(element, picker, options, rootElement);
          var children = childLayouts.map(function (layout) {
            return layout(picker, options, rootElement);
          });
          appendChildren(element, children);
          return element;
        };
      };
    };
  };

  var createBoxLayout = createLayout(function () {
    return createDocumentElement({
      tagName: 'div'
    });
  });
  var createButtonLayout = createLayout(function (picker, rootElement) {
    var buttonElement = createDocumentElement({
      tagName: 'button'
    });
    rootElement.addEventListener(EVENT_NAME.INPUT_OPEN, function () {
      buttonElement.disabled = false;
    });
    rootElement.addEventListener(EVENT_NAME.INPUT_CLOSE, function () {
      buttonElement.disabled = true;
    });
    return buttonElement;
  }); ////////////////
  // COMPONENTS //
  ////////////////

  var createInputRootBox = createBoxLayout(function (element, picker, options, rootElement) {
    if (!(rootElement instanceof HTMLInputElement)) {
      throw new Error('Cannot initialize an input root without the input element');
    }

    initializeInput(rootElement, picker);
    element.className = options.className.inputRoot;
    element.addEventListener('focusin', function (event) {
      return event.stopPropagation();
    });
    element.addEventListener('click', function (event) {
      return event.stopPropagation();
    });
    element.addEventListener('mousedown', function (event) {
      var target = event.target;

      if (target instanceof HTMLInputElement || target instanceof HTMLButtonElement) {
        return;
      }

      event.preventDefault();
    });
  });
  var createRootBox = createBoxLayout(function (element, picker, options, rootElement) {
    addAttributes(element, {
      className: options.className.root
    });
    rootElement.addEventListener(EVENT_NAME.INPUT_OPEN, function () {
      element.classList.add(options.className.root__active);
    });
    rootElement.addEventListener(EVENT_NAME.INPUT_CLOSE, function () {
      element.classList.remove(options.className.root__active);
    });
    picker.subscribeToSelection(function () {
      return dispatchEvent(rootElement, EVENT_NAME.CHANGE);
    });
  });
  var createHeaderBox = createBoxLayout(function (element, picker, options) {
    addAttributes(element, {
      className: options.className.header
    });
  });
  var createBodyBox = createBoxLayout(function (element, picker, options) {
    addAttributes(element, {
      className: options.className.body
    });
  });
  var createFooterBox = createBoxLayout(function (element, picker, options) {
    addAttributes(element, {
      className: options.className.footer
    });
  });
  var createTimeBox = createBoxLayout(function (element, picker, options) {
    // $FlowFixMe
    var state = picker.store.getState();
    addAttributes(element, {
      className: options.className.time,
      hidden: !state.selected
    });
    picker.store.subscribe(function () {
      var state = picker.store.getState();
      element.hidden = !state.selected;
    });
  });
  var createMonthAndYearBox = createBoxLayout(function (element, picker, options) {
    addAttributes(element, {
      className: options.className.monthAndYear
    });

    var render = function render() {
      var state = picker.store.getState();
      element.innerHTML = '';
      appendChildren(element, [createDocumentElement({
        children: state.templateHookWords.MMMM[state.view.getMonth()],
        className: options.className.monthAndYear_month
      }), createDocumentElement({
        children: state.view.getFullYear(),
        className: options.className.monthAndYear_year
      })]);
    };

    render();
    picker.store.subscribe(render);
  });
  var createPreviousButton = createButtonLayout(function (element, picker, options) {
    var onMouseDown = function onMouseDown(event) {
      return event.preventDefault();
    };

    var onClick = function onClick() {
      return picker.viewPrevious();
    };

    addAttributes(element, {
      className: options.className.button_previous,
      onMouseDown: onMouseDown,
      onClick: onClick
    });
    element.innerHTML = chevronLeft;
  });
  var createTodayButton = createButtonLayout(function (element, picker, options) {
    var onMouseDown = function onMouseDown(event) {
      return event.preventDefault();
    };

    var onClick = function onClick() {
      return picker.viewToday();
    };

    addAttributes(element, {
      className: options.className.button_today,
      onMouseDown: onMouseDown,
      onClick: onClick
    });
    element.innerHTML = bullsEye;
  });
  var createNextButton = createButtonLayout(function (element, picker, options) {
    var onMouseDown = function onMouseDown(event) {
      return event.preventDefault();
    };

    var onClick = function onClick() {
      return picker.viewNext();
    };

    addAttributes(element, {
      className: options.className.button_next,
      onMouseDown: onMouseDown,
      onClick: onClick
    });
    element.innerHTML = chevronRight;
  });
  var createGridButton = createButtonLayout(function (element, picker, options, rootElement) {
    rootElement.addEventListener(EVENT_NAME.INPUT_ACTIVE, function () {
      element.classList.add(options.className.grid__focused);
    });
    rootElement.addEventListener(EVENT_NAME.INPUT_INACTIVE, function () {
      element.classList.remove(options.className.grid__focused);
    });

    var onClick = function onClick(event) {
      var value = getValueFromMouseEvent(event);

      if (value) {
        picker.setSelected({
          value: value
        });
      }
    };

    addAttributes(element, {
      className: options.className.grid,
      onClick: onClick,
      onKeyDown: createOnKeyDownCalendar(picker)
    });

    var render = function render() {
      element.innerHTML = '';
      appendChildren(element, createGridCellElements(options, picker.store.getState()));
    };

    render();
    picker.store.subscribe(render);
  });
  var createMeridiemButton = createButtonLayout(function (element, picker, options) {
    var onMouseDown = function onMouseDown(event) {
      return event.preventDefault();
    };

    var onClick = function onClick() {
      return picker.cycleMeridiem();
    };

    addAttributes(element, {
      className: options.className.button_meridiem,
      onMouseDown: onMouseDown,
      onClick: onClick
    });

    var updateButton = function updateButton() {
      var _picker$store$getStat = picker.store.getState(),
          selected = _picker$store$getStat.selected;

      element.innerHTML = '';
      appendChildren(element, selected ? selected.getHours() < 12 ? 'AM' : 'PM' : 'AM');
      element.tabIndex = selected ? 0 : -1;
    };

    updateButton();
    picker.store.subscribe(updateButton);
  });
  var createHourInput = createBoxLayout(function (element, picker, options) {
    addAttributes(element, {
      className: options.className.time_hours
    });

    var cycleUp = function cycleUp() {
      return picker.cycleHourUp();
    };

    var cycleDown = function cycleDown() {
      return picker.cycleHourDown();
    };

    var hourInput = createTimeInputElement(options, {
      className: options.className.time_input__hours,
      onKeyCodeUp: cycleUp,
      onKeyCodeDown: cycleDown
    });

    var updateHourInput = function updateHourInput() {
      var state = picker.store.getState();
      var hours = state.selected ? padZero(state.selected.getHours() % 12 || 12, 2) : '';
      hourInput.value = hours;
      hourInput.tabIndex = state.selected ? 0 : -1;
    };

    updateHourInput();
    picker.store.subscribe(updateHourInput);
    appendChildren(element, [hourInput].concat(_toConsumableArray(createTimeCounterElements(options, {
      onClickUp: cycleUp,
      onClickDown: cycleDown
    }))));
  });
  var createMinuteInput = createBoxLayout(function (element, picker, options) {
    addAttributes(element, {
      className: options.className.time_minutes
    });

    var cycleUp = function cycleUp() {
      return picker.cycleMinuteUp();
    };

    var cycleDown = function cycleDown() {
      return picker.cycleMinuteDown();
    };

    var minuteInput = createTimeInputElement(options, {
      className: options.className.time_input__minutes,
      onKeyCodeUp: cycleUp,
      onKeyCodeDown: cycleDown
    });

    var updateMinuteInput = function updateMinuteInput() {
      var state = picker.store.getState();
      var minutes = state.selected ? padZero(state.selected.getMinutes(), 2) : '';
      minuteInput.value = minutes;
      minuteInput.tabIndex = state.selected ? 0 : -1;
    };

    updateMinuteInput();
    picker.store.subscribe(updateMinuteInput);
    appendChildren(element, [minuteInput].concat(_toConsumableArray(createTimeCounterElements(options, {
      onClickUp: cycleUp,
      onClickDown: cycleDown
    }))));
  });
  var createTimeSeparator = createBoxLayout(function (element, picker, options) {
    addAttributes(element, {
      className: options.className.time_separator
    });
    element.innerHTML = ':';
  }); //////////////
  // ELEMENTS //
  //////////////

  var createGridCellElements = function createGridCellElements(options, state) {
    var datesInWeeks = getDatesInWeeks(state.view, state.firstDayOfWeek);
    var elements = datesInWeeks.map(function (datesInWeek) {
      return createDocumentElement({
        className: options.className.grid_row,
        children: datesInWeek.map(function (dateObject) {
          return createGridCellElement(options, state, dateObject);
        })
      });
    });
    elements.unshift(createDocumentElement({
      className: flatten([options.className.grid_row, options.className.grid_row__label]),
      children: getWeekdayLabels(state.templateHookWords.DDD, state.firstDayOfWeek).map(function (weekday) {
        return createDocumentElement({
          className: options.className.grid_label,
          children: weekday
        });
      })
    }));
    return elements;
  };

  var createGridCellElement = function createGridCellElement(options, state, dateObject) {
    var _ref;

    var cellInfo = computeInfo(state, dateObject);
    var parent = createDocumentElement({
      className: flatten([options.className.grid_cell, (_ref = {}, _defineProperty(_ref, options.className.grid_cell__today, cellInfo.isToday), _defineProperty(_ref, options.className.grid_cell__highlighted, cellInfo.isHighlighted), _defineProperty(_ref, options.className.grid_cell__selected, cellInfo.isSelected), _defineProperty(_ref, options.className.grid_cell__outOfView, !cellInfo.isSameMonth), _defineProperty(_ref, options.className.grid_cell__disabled, cellInfo.isDisabled), _ref)]),
      attributes: {
        dataset: cellInfo.isDisabled ? undefined : {
          value: dateObject.getTime()
        }
      }
    });
    var children = createDocumentElement({
      children: dateObject.getDate()
    });
    var content = options.renderCell({
      dateObject: dateObject,
      children: children
    }) || children;
    appendChildren(parent, content);
    return parent;
  };

  var createTimeInputElement = function createTimeInputElement(options, _ref2) {
    var className = _ref2.className,
        onKeyCodeUp = _ref2.onKeyCodeUp,
        onKeyCodeDown = _ref2.onKeyCodeDown;

    var onKeyDown = function onKeyDown(event) {
      if (event.keyCode === KEY_CODE.UP) {
        event.preventDefault();
        onKeyCodeUp();
      } else if (event.keyCode === KEY_CODE.DOWN) {
        event.preventDefault();
        onKeyCodeDown();
      }
    };

    var onFocus = function onFocus() {
      return inputElement.setSelectionRange(0, 0);
    };

    var inputElement = createDocumentElement({
      tagName: 'input',
      className: [options.className.time_input, className],
      attributes: {
        onKeyDown: onKeyDown,
        onFocus: onFocus,
        readOnly: true
      }
    });
    return inputElement;
  };

  var createTimeCounterElements = function createTimeCounterElements(options, _ref3) {
    var onClickUp = _ref3.onClickUp,
        onClickDown = _ref3.onClickDown;

    var onMouseDown = function onMouseDown(event) {
      return event.preventDefault();
    };

    var upElement = createDocumentElement({
      tagName: 'button',
      className: [options.className.time_counter, options.className.time_counter__up],
      attributes: {
        tabIndex: -1,
        onMouseDown: onMouseDown,
        onClick: onClickUp
      }
    });
    upElement.innerHTML = chevronUp;
    var downElement = createDocumentElement({
      tagName: 'button',
      className: [options.className.time_counter, options.className.time_counter__down],
      attributes: {
        tabIndex: -1,
        onMouseDown: onMouseDown,
        onClick: onClickDown
      }
    });
    downElement.innerHTML = chevronDown;
    return [upElement, downElement];
  }; //////////////////
  // INITIALIZERS //
  //////////////////


  var initializeInput = function initializeInput(inputElement, picker) {
    inputElement.readOnly = true;
    inputElement.value = picker.getValue();
    inputElement.addEventListener('focus', function () {
      dispatchEvent(inputElement, EVENT_NAME.INPUT_OPEN);
      dispatchEvent(inputElement, EVENT_NAME.INPUT_ACTIVE);
    });
    inputElement.addEventListener('blur', function () {
      dispatchEvent(inputElement, EVENT_NAME.INPUT_INACTIVE);
    });
    inputElement.addEventListener('keydown', createOnKeyDownCalendar(picker));

    var onDocumentEvent = function onDocumentEvent(event) {
      if (event.target !== inputElement && // $FlowFixMe: In Chrome 62+, password auto-fill simulates focusin
      !event.isSimulated && // In Firefox, a click on an `option` element bubbles up directly
      // to the document.
      event.target !== document && // In Firefox stopPropagation() doesn’t prevent right-click events
      // from bubbling. So make sure the event wasn’t a right-click.
      // $FlowFixMe
      event.which !== 3) {
        dispatchEvent(inputElement, EVENT_NAME.INPUT_CLOSE);
      }
    };

    document.addEventListener('focusin', onDocumentEvent);
    document.addEventListener('click', onDocumentEvent);
    inputElement.addEventListener(EVENT_NAME.UNMOUNT, function () {
      document.removeEventListener('focusin', onDocumentEvent);
      document.removeEventListener('click', onDocumentEvent);
    });
    dispatchEvent(inputElement, EVENT_NAME.INPUT_CLOSE);
    picker.subscribeToSelection(function (_ref4) {
      var value = _ref4.value;

      if (value === inputElement.value) {
        return;
      }

      inputElement.value = value;
      var event = new CustomEvent('change');
      inputElement.dispatchEvent(event);
    });
  }; /////////////
  // HELPERS //
  /////////////


  var createDocumentElement = function createDocumentElement(_ref5) {
    var _ref5$tagName = _ref5.tagName,
        tagName = _ref5$tagName === void 0 ? 'div' : _ref5$tagName,
        className = _ref5.className,
        attributes = _ref5.attributes,
        children = _ref5.children;
    var element = document.createElement(tagName);
    addAttributes(element, _objectSpread({}, attributes, {
      className: flatten(['pickadate--element', className])
    }));
    appendChildren(element, children);
    return element;
  };

  var appendChildren = function appendChildren(element, children) {
    if (children == null) {
      return;
    }

    children = Array.isArray(children) ? children : [children];
    children.forEach(function (child) {
      if (!(child instanceof Node)) {
        child = typeof child !== 'string' ? child.toString() : child;
        child = document.createTextNode(child);
      }

      element.appendChild(child);
    });
  };

  var addAttributes = function addAttributes(element, attributes) {
    if (!attributes) {
      return;
    }

    Object.keys(attributes).forEach(function (attributeName) {
      // $FlowFixMe
      var attributeValue = attributes[attributeName];

      if (attributeValue == null) {
        return;
      }

      if (typeof attributeValue === 'function') {
        element.addEventListener(attributeName.replace(/^on/, '').toLowerCase(), attributeValue);
        return;
      }

      if (attributeName === 'className') {
        // $FlowFixMe
        addClassName(element, attributeValue);
        return;
      }

      if (attributeName === 'dataset') {
        // $FlowFixMe
        Object.keys(attributeValue).forEach(function (datasetName) {
          var fullDatasetName = "data-".concat(caseDash(datasetName)); // $FlowFixMe

          element.setAttribute(fullDatasetName, attributeValue[datasetName]);
        });
        return;
      } // $FlowFixMe


      element.setAttribute(attributeName, attributeValue);
    });
  };

  var addClassName = function addClassName(element, className) {
    if (!className) {
      return;
    }

    var list = Array.isArray(className) ? className : [className];
    list.forEach(function (item) {
      if (!item) {
        return;
      }

      if (typeof item === 'string') {
        item = _defineProperty({}, item, true);
      }

      if (Array.isArray(item)) {
        item.forEach(function (className) {
          if (className && typeof className === 'string') {
            element.classList.add(className);
          }
        });
        return;
      }

      Object.keys(item).forEach(function (className) {
        // $FlowFixMe
        if (className && typeof className === 'string' && item[className]) {
          element.classList.add(className);
        }
      });
    });
  };

  var dispatchEvent = function dispatchEvent(element, eventName) {
    var customEvent = new CustomEvent(eventName);
    element.dispatchEvent(customEvent);
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

  var RENDER_OPTIONS = {
    mode: 'date',
    renderCell: function renderCell() {},
    className: CLASS_NAME_MAP
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

  var renderInputRootLayout = createInputRootBox();
  var renderRootLayout = createRootBox();
  var renderHeaderLayout = createHeaderBox(createMonthAndYearBox(), createPreviousButton(), createTodayButton(), createNextButton());
  var renderBodyLayout = createBodyBox(createGridButton());
  var renderFooterLayout = createFooterBox(createTimeBox(createHourInput(), createTimeSeparator(), createMinuteInput(), createMeridiemButton()) // renderer.createClearButton()
  );

  var renderContainerLayout = function renderContainerLayout(picker, options, element) {
    var rootElement = renderRootLayout(picker, options, element);
    var headerElement = renderHeaderLayout(picker, options, element);
    var bodyElement = renderBodyLayout(picker, options, element);
    appendChildren(rootElement, [headerElement, bodyElement]);

    if (options.mode === 'date-time') {
      var footerElement = renderFooterLayout(picker, options, element);
      appendChildren(rootElement, footerElement);
    }

    return rootElement;
  };

  var pickadateSymbol = Symbol('pickadate');

  var render = function render(element, picker, options) {
    // $FlowFixMe
    var existingPicker = element[pickadateSymbol];

    if (existingPicker) {
      throw new Error('A picker is already rendered to this element');
    }

    options = options ? mergeUpdates(RENDER_OPTIONS, options) : RENDER_OPTIONS;
    var containerElement = renderContainerLayout(picker, options, element);

    if (element instanceof HTMLInputElement) {
      var rootElement = renderInputRootLayout(picker, options, element);
      rootElement.appendChild(containerElement);
      element.insertAdjacentElement('afterend', rootElement);
    } else {
      element.appendChild(containerElement);
    }

    Object.defineProperty(element, pickadateSymbol, {
      value: picker
    });
    dispatchEvent(element, EVENT_NAME.MOUNT);
  }; //////////////
  // UNRENDER //
  //////////////


  var removeFromDom = function removeFromDom(element) {
    if (element && element.parentNode) {
      element.parentNode.removeChild(element);
    }
  };

  var unrender = function unrender(element) {
    // $FlowFixMe
    var elementProp = element[pickadateSymbol];
    var picker = elementProp;

    if (!picker) {
      return;
    }

    if (element instanceof HTMLInputElement) {
      var rootElement = element.nextElementSibling;
      removeFromDom(rootElement);
    } else {
      removeFromDom(element);
    } // $FlowFixMe


    delete element[pickadateSymbol];
    dispatchEvent(element, EVENT_NAME.UNMOUNT);
  };

  var vanilla = {
    create: createPicker,
    render: render,
    unrender: unrender
  };

  return vanilla;

}));

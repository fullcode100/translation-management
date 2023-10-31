export function throttle(func, delay) {
  let timerId = 0;

  return (...args) => {
    if (timerId) {
      clearTimeout(timerId);
      timerId = 0;
    }
    timerId = setTimeout(() => {
      func(...args);
      timerId = 0;
    }, delay);
  };
}

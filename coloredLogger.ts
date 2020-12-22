/**
 * A fancy colored console logger to make it easier to see custom messages
 */
export const coloredLogger = {
  /**
   * Creates a fancy colored color logger to route through
   * @param name The name of the logger, usually the file or component name
   * @param color The color your want it to appear as in the console output
   * @example
   *  private console: Console = mcLogger.getConsole('count-hub.ts', 'cornflowerblue');
   *  this.console.debug('Your message');
   */
  getConsole(name: string, color: string) {
    const newConsole : Console = Object.create(console);
    newConsole.debug = console.debug.bind(console, `%c[${name}]`, `color: ${color}`);
    newConsole.info = console.info.bind(console, `%c[${name}]`, `color: ${color}`);
    newConsole.log = console.log.bind(console, `%c[${name}]`, `color: ${color}`);
    newConsole.warn = console.warn.bind(console, `[${name}]`);
    newConsole.error = console.error.bind(console, `[${name}]`);
    newConsole.time = (label?: string) => console.time(`[${name}] ${label}`);
    newConsole.timeEnd = (label?: string) => console.timeEnd(`[${name}] ${label}`);

    return newConsole;
  }
}

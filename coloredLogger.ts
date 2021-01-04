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
  },
  
  /**
   * Creates a fancy colored logger ot route through. Uses the the name to create a HEX color.
   * @param name The name of the logger, usually the file or component name
   * @example
   *  private console: Console = mcLogger.getConsole(this.constructor.name);
   *  this.console.debug('Your message');
   */
  getConsoleWithAutoColor(name: string): Console {
    // Convert the name into a hex string
    let textEncoder = new TextEncoder();
    let utf8ByteArray = textEncoder.encode(name);
    let hexString = this.toHexString(utf8ByteArray);
    // The hexString will be too long for a valid color, so we take the first 6 characters after '0x', and put a '#' at the front
    let trimmedHexString = `#${hexString.substring(2, 8)}`;

    return this.getConsole(name, trimmedHexString);
  },

  toHexString(byteArray: Uint8Array) {
    var s = '0x';
    byteArray.forEach(function(byte) {
      s += ('0' + (byte & 0xFF).toString(16)).slice(-2);
    });
    return s;
  }
}

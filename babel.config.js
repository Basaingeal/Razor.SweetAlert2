module.exports = function(api) {
  if (api) {
    api.cache(true);
  }

  const presets = [
    [
      "@babel/preset-env",
      {
        useBuiltIns: false,
        debug: false,
        corejs: 3
      }
    ],
    "@babel/preset-typescript"
  ];

  return { presets };
};

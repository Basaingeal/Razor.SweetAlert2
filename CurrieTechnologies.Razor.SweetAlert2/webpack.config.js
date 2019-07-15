const path = require('path');
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require('optimize-css-assets-webpack-plugin');
const TerserJSPlugin = require('terser-webpack-plugin');
const { CleanWebpackPlugin } = require('clean-webpack-plugin');
const FixStyleOnlyEntriesPlugin = require("webpack-fix-style-only-entries");

module.exports = {
  entry: {
    sweetAlert2: './src/ts/SweetAlert.ts',
    'sweetAlert2.min': './src/ts/SweetAlert.ts',
    darkTheme: './src/scss/dark-theme.scss',
    'darkTheme.min': './src/scss/dark-theme.scss',
    minimalTheme: './src/scss/minimal-theme.scss',
    'minimalTheme.min': './src/scss/minimal-theme.scss',
    borderlessTheme: './src/scss/borderless-theme.scss',
    'borderlessTheme.min': './src/scss/borderless-theme.scss'
  },
  output: {
    filename: "[name].js",
    path: path.resolve(__dirname, 'wwwroot')
  },
  module: {
    rules: [
      {
        test: /\.ts$/,
        use: 'ts-loader',
        exclude: /node_modules/
      },
      {
        test: /\.(sass|scss)$/,
        use: [
          MiniCssExtractPlugin.loader,
          "css-loader",
          "sass-loader"
        ],
        exclude: /node_modules/
      }
    ]
  },
  mode: 'production',
  plugins: [
    new FixStyleOnlyEntriesPlugin(),
    new MiniCssExtractPlugin({
      filename: "[name].css",
      path: path.resolve(__dirname, 'wwwroot')
    }),
    new CleanWebpackPlugin()
  ],
  optimization: {
    minimizer: [
      new TerserJSPlugin({
        include: /\.min\.js$/
      }),
      new OptimizeCSSAssetsPlugin({
        assetNameRegExp: /\.min\.css$/
      })]
  }
};
const path = require("path");
const MiniCssExtractPlugin = require("mini-css-extract-plugin");
const OptimizeCSSAssetsPlugin = require("optimize-css-assets-webpack-plugin");
const TerserJSPlugin = require("terser-webpack-plugin");
const { CleanWebpackPlugin } = require("clean-webpack-plugin");
const FixStyleOnlyEntriesPlugin = require("webpack-fix-style-only-entries");
const pkg = require("./package.json");
const babelConfig = require("./babel.config.js");

function getIECompatBabelLoaderOptions() {
  const babelIECompatConfig = babelConfig();
  const babelPresetEnvOptions = babelIECompatConfig.presets[0][1];
  babelPresetEnvOptions.useBuiltIns = "usage";
  babelPresetEnvOptions.targets = [...pkg.browserslist, "ie 11"].join(", ");
  return babelIECompatConfig;
}

module.exports = [
  {
    entry: {
      sweetAlert2: "./src/ts/SweetAlert.ts",
      "sweetAlert2.min": "./src/ts/SweetAlert.ts",
      defaultTheme: "./src/scss/default-theme.scss",
      "defaultTheme.min": "./src/scss/default-theme.scss",
      darkTheme: "./src/scss/dark-theme.scss",
      "darkTheme.min": "./src/scss/dark-theme.scss",
      minimalTheme: "./src/scss/minimal-theme.scss",
      "minimalTheme.min": "./src/scss/minimal-theme.scss",
      borderlessTheme: "./src/scss/borderless-theme.scss",
      "borderlessTheme.min": "./src/scss/borderless-theme.scss",
      bootstrap4Theme: "./src/scss/bootstrap-4-theme.scss",
      "bootstrap4Theme.min": "./src/scss/bootstrap-4-theme.scss",
      materialUITheme: "./src/scss/material-ui-theme.scss",
      "materialUITheme.min": "./src/scss/material-ui-theme.scss",
      wordpressAdminTheme: "./src/scss/wordpress-admin-theme.scss",
      "wordpressAdminTheme.min": "./src/scss/wordpress-admin-theme.scss",
    },
    output: {
      filename: "[name].js",
      path: path.resolve(__dirname, "wwwroot")
    },
    module: {
      rules: [
        {
          test: /\.ts$/,
          use: "babel-loader",
          exclude: /node_modules/
        },
        {
          test: /\.(sass|scss)$/,
          use: [MiniCssExtractPlugin.loader, "css-loader", "sass-loader"],
          exclude: /node_modules/
        }
      ]
    },
    plugins: [
      new FixStyleOnlyEntriesPlugin(),
      new MiniCssExtractPlugin({
        filename: "[name].css",
        path: path.resolve(__dirname, "wwwroot")
      }),
      new CleanWebpackPlugin({
        cleanOnceBeforeBuildPatterns: []
      })
    ],
    optimization: {
      minimizer: [
        new TerserJSPlugin({
          include: /\.min\.js$/
        }),
        new OptimizeCSSAssetsPlugin({
          assetNameRegExp: /\.min\.css$/
        })
      ]
    },
    resolve: {
      extensions: [".ts", ".scss", ".js"]
    }
  },
  {
    entry: {
      "sweetAlert2.ieCompat": "./src/ts/SweetAlert.ts",
      "sweetAlert2.ieCompat.min": "./src/ts/SweetAlert.ts"
    },
    output: {
      filename: "[name].js",
      path: path.resolve(__dirname, "wwwroot")
    },
    module: {
      rules: [
        {
          test: /\.ts$/,
          use: {
            loader: "babel-loader",
            options: getIECompatBabelLoaderOptions()
          },
          exclude: /node_modules/
        },
        {
          test: /\.(sass|scss)$/,
          use: [MiniCssExtractPlugin.loader, "css-loader", "sass-loader"],
          exclude: /node_modules/
        }
      ]
    },
    optimization: {
      minimizer: [
        new TerserJSPlugin({
          include: /\.min\.js$/
        })
      ]
    },
    resolve: {
      extensions: [".ts", ".js"]
    }
  }
];

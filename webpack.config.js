import path, { dirname } from "path";
import { fileURLToPath } from "url";
import MiniCssExtractPlugin from "mini-css-extract-plugin";
import CssMinimizerPlugin from "css-minimizer-webpack-plugin";
import TerserJSPlugin from "terser-webpack-plugin";

const __dirname = dirname(fileURLToPath(import.meta.url));

export default {
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
    bulmaTheme: "./src/scss/bulma-theme.scss",
    "bulmaTheme.min": "./src/scss/bulma-theme.scss",
  },
  output: {
    filename: "[name].js",
    path: path.resolve(__dirname, "wwwroot"),
  },
  module: {
    rules: [
      {
        test: /\.ts$/,
        use: "babel-loader",
        exclude: /node_modules/,
      },
      {
        test: /\.(sass|scss)$/,
        use: [MiniCssExtractPlugin.loader, "css-loader", "sass-loader"],
        exclude: /node_modules/,
      },
    ],
  },
  plugins: [
    new MiniCssExtractPlugin({
      filename: "[name].css",
    }),
  ],
  optimization: {
    minimizer: [
      new TerserJSPlugin({
        include: /\.min\.js$/,
      }),
      new CssMinimizerPlugin(),
    ],
  },
  resolve: {
    extensions: [".ts", ".scss", ".js"],
  },
  stats: "normal",
};

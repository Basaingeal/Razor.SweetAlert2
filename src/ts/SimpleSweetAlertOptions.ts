import { SweetAlertOptions } from "sweetalert2";
import { SweetAlertGrow } from "./SweetAlertGrow";

type SimpleSweetAlertOptions = Omit<
  SweetAlertOptions,
  | "willOpen"
  | "didClose"
  | "didDestroy"
  | "didOpen"
  | "willClose"
  | "didRender"
  | "grow"
  | "width"
> & {
  willOpen: boolean;

  didClose: boolean;

  didDestroy: boolean;

  didOpen: boolean;

  willClose: boolean;

  didRender: boolean;

  grow?: SweetAlertGrow;

  width?: string;
};

export default SimpleSweetAlertOptions;

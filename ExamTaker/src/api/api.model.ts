export class GetSingleModel<T> {
  data!: T;
}

export class CreateResponseModel {
  data!: {
    id: string;
  };
}

export class ListAllModel<T> {
  data: T[] = [];
  pages?: number;
}

export class ListAllParameters {
  page?: number;
  "page-size"?: number;
}



export class ListAllModel<T> {
    data: T[] = [];
    pages?: number;
}

export class ListAllParameters {
    page?: number;
    "page-size"?: number;
}

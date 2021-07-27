export class ShopParams {
    brandId: number = 0; // initial value of 0 so "All" options starts off as selected
    typeId: number = 0;
    sort = 'name';
    pageNumber = 1;
    pageSize = 6;
    search: string;
}
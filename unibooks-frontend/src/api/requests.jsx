import axios from "./axios";

export const fetchAdverts = async(query) =>
{
    try{
            const queryParams = {
                SortBy: query.SortBy || undefined,
                title: query.title || undefined,
                publisher: query.publisher || undefined,
                isDescending: query.isDescending || undefined,
            };

            const response = await axios.get('http://localhost:5222/api/advert',
                {
                    params: queryParams,
                    headers:  { 'Accept': '*/*'},
                }
            );

            return response.data;
    }
    catch
    {

    }
}
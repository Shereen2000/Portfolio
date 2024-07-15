import React, {useState, useEffect} from 'react'
import AdvertCard from './AdvertCard'
import '../components/styles.css'
import { fetchAdverts } from '../api/requests'

const AdvertList = ({searchBy, filters}) => {

  const [adverts, setAdverts] = useState([]);


 // console.log(searchBy)
  console.log(filters)

  useEffect(() => {
    const fetchData = async () => {
      try {
        const query = {
          sortBy: null,
          title: null,
          publisher: null,
          isDescending: false,
        };
        const fetchedAdverts = await fetchAdverts(query);
        setAdverts(fetchedAdverts);

        //console.log(fetchedAdverts)

      } catch (error) {
        console.error('Error fetching adverts:', error);
      }
    };

    fetchData();
  }, []); 


  return (
    <div className='p-5 advert-list-container overflow-y-auto w-full'>
    
          {adverts.map((advert, index) => (
            <AdvertCard key={index} advert = {advert} />
          ))}
    
    </div>
  )
}

export default AdvertList 

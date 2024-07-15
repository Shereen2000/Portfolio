import React, {useState} from 'react'
import AdvertList from '../components/AdvertList'
import Search from '../components/Search'
import Filter from '../components/Filter'
import '../components/styles.css'

const HomePage = () => {
   const [searchBy, setSearchBy] = useState('Title')
   const [filters, setFilters] = useState({});

   const handleFilterChange = (newFilters) => {
    setFilters(newFilters); };
  
  return (
    <div className=''>
      
      <Search setSearchBy = {setSearchBy}/>
      <div className='md:flex p-5 w-full  bg-gray-100'>
            <Filter onFilterChange={handleFilterChange}/>
            <AdvertList searchBy = {searchBy} filters = {filters}/>
      </div>
     
    </div>
  )
}

export default HomePage

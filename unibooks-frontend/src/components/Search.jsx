import React, {useState} from 'react'
import {HiMagnifyingGlass} from 'react-icons/hi2';

const Search = ({setSearchBy}) => {

    const [searchByLocal, setSearchByLocal] = useState('Title');

    const handleSelectChange = (e) => {
        const selectedValue = e.target.value;
        setSearchByLocal(selectedValue);
        setSearchBy(selectedValue);
      };


  return (
    <div className='p-5 h-fit'>
            <div className='flex p-5 items-center justify-center gap-4'> 
                    <select value={searchByLocal} onChange={handleSelectChange}>
                        <option value="Title" >Title</option>
                        <option value="Author">Author</option>
                        <option value="Isbn">Isbn</option>
                    </select>
                    <input type="text" className='border border-solid rounded-full w-full md:w-96 p-2' placeholder={`Search ${searchByLocal}`} />
                        <HiMagnifyingGlass/>
            </div>
      
    </div>
  )
}

export default Search

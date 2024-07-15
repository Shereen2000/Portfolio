import React from 'react'
import { FaBookmark } from 'react-icons/fa';

const AdvertCard = ({advert}) => {

  return (
    <div className='p-4 flex border border-gray-200 rounded-lg shadow-md mb-4 hover:border-[3px] w-full'>
      <div className='pr-8'>
            <div>  
              <img className='w-[150px] md:w-[300px] rounded-lg hover:border-[3px]' 
              src={advert.coverUrl}></img>
            </div>
      </div>
    
      <div className="flex flex-col justify-between w-full">
        <div>
            <p className='text-xl md:text-3xl font-semibold mb-2'>{advert.title}</p>
            <p className='text-lg md:text-xl mb-2'>Disney Books</p>
            <p className='text-lg md:text-xl mb-2'>ISBN {advert.isbn} </p>
            <p className='text-lg md:text-xl mb-2'>Tumiso Mokautu</p>
            <p className='text-lg md:text-xl mb-2 font-semibold'>R{advert.price}</p>
            <p className='text-lg md:text-xl mb-2'>{advert.condition}</p>
        </div>
          
            <div className='flex justify-end'>
                <FaBookmark className='text-blue-500 text-2xl' />
            </div>
      </div>

      

    </div>
  )
}

export default AdvertCard

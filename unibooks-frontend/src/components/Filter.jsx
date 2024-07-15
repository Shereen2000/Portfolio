
import React,{useState}from 'react'

const Institutions = [
    'University of the Free State',
    'University of Pretoria',
    'University of Cape Town',
    'Tshwane University of Technology'
]



const Filter = ({onFilterChange}) => {

  const[minPrice, setMinPrice] = useState(0)
  const[maxPrice, setMaxPrice] = useState(1000000)
  const [selectedInstitution, setSelectedInstitution] = useState('');
  const [showWarning, setShowWarning] = useState(false);

  const handleFilter = () => {

    if(maxPrice !== '' && parseFloat(maxPrice) < parseFloat(minPrice))
    {
      setShowWarning(true);
      return;
    }
    const filters = {
      institution: selectedInstitution,
      minPrice: parseFloat(minPrice),
      maxPrice: parseFloat(maxPrice)
    };
    onFilterChange(filters);
  };

  const handleReset = () => {
   
    setSelectedInstitution('');
    setMinPrice(0);
    setMaxPrice(100000);
    setShowWarning(false); 
    onFilterChange({});
  };


  const numericalOptions = [
    { value: '', label: 'Any' },
    { value: '50', label: '$50' },
    { value: '100', label: '$100' },
    { value: '200', label: '$200' },
    { value: '300', label: '$300' },
    { value: '400', label: '$400' },
    { value: '500', label: '$500' }
    // Add more options as needed
  ];

  const handleMaxPriceChange = (e) => {
    const value = e.target.value;
    if (value === '') {
      setMaxPrice(100000); 
    } else {
      setMaxPrice(value); 
    }
      setShowWarning(false);
  };

  const handleMinPriceChange = (e) => {
    const value = e.target.value;
    if (value === '') {
      setMinPrice(0); 
    } else {
      setMinPrice(value); 
    }
      setShowWarning(false);
  };

  
  return (
    <div className='mb-5'>
      
      <div className='mb-3'>
                <select className='block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm'
                 value={selectedInstitution}
                 onChange={(e) => setSelectedInstitution(e.target.value)}>
                          <option value=''>Filter by School</option>
                                  {Institutions.map((institution, index) => (
                                    <option key={index} value={institution}>
                                      {institution}
                            </option>
                          ))}
                </select>
      </div>
      <div className='grid grid-cols-1 md:grid-cols-2 gap-4'>
                <div>
                <label className='block mb-1'>
                          Min Price: 
                        </label>
                        <select
                          value={minPrice}
                          onChange={handleMinPriceChange}
                          className='block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm'
                        >
                          {numericalOptions.map((option, index) => (
                            <option key={index} value={option.value}>
                              {option.label}
                            </option>
                          ))}
                        </select>
                </div>

                <div>
                          
                        <label className='block mb-1'>
                          Max Price: 
                        </label>
                        <select
                          value={maxPrice}
                          onChange={handleMaxPriceChange}
                          className='block w-full px-4 py-2 border border-gray-300 rounded-md shadow-sm focus:outline-none focus:ring-blue-500 focus:border-blue-500 sm:text-sm'
                        >
                          {numericalOptions.map((option, index) => (
                            <option key={index} value={option.value}>
                              {option.label}
                            </option>
                          ))}
                        </select>
                </div> 
      </div>
      <div>
                   {showWarning && (
                  <p className='text-red-500 text-sm mt-1'>
                    Maximum price cannot be less than minimum price
                  </p>
                  )}
      </div>


      <div className='flex justify-center mt-4'>
              <button
                className='px-4 py-2 bg-blue-500 text-white rounded-md shadow-sm hover:bg-blue-600 focus:outline-none focus:ring-2 focus:ring-blue-500 focus:ring-opacity-50'
                onClick={handleFilter}
              >
                Filter
              </button>
              <button
                className='ml-2 px-4 py-2 bg-gray-300 text-gray-700 rounded-md shadow-sm hover:bg-gray-400 focus:outline-none focus:ring-2 focus:ring-gray-300 focus:ring-opacity-50'
                onClick={handleReset}
              >
                Reset
              </button>
      </div>
   
    </div>
  )
}

export default Filter

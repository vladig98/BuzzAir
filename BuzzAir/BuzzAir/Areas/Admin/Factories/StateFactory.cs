namespace BuzzAir.Areas.Admin.Factories
{
    public static class StateFactory
    {
        public static CreateStateVM InitializeCreateCityViewModel(List<SelectListItem> countries)
        {
            CreateStateVM model = new()
            {
                CountryOptions = countries
            };

            return model;
        }

        public static void UpdateEditViewModelWithSelects(EditStateVM model, List<SelectListItem> countries)
        {
            model.Countries = countries;
        }

        public static State Create(CreateStateVM model)
        {
            State state = new()
            {
                Name = model.Name,
                Country = model.Country!
            };

            return state;
        }

        public static PaginatedList<StateDTO> GetPaginatedList(int pageNumber, long count, List<State> states)
        {
            List<StateDTO> dtos = MapModelToDTO(states);
            PaginatedList<StateDTO> paginatedList = new(dtos, count, pageNumber, GlobalConstants.ItemsPerPage);

            return paginatedList;
        }

        public static void Update(State state, EditStateVM model, bool canChangeLocation)
        {
            state.Name = model.Name;

            if (!canChangeLocation)
            {
                return;
            }

            state.Country = model.Country!;
        }

        public static DeleteStateVM GetDeleteViewModel(State state)
        {
            DeleteStateVM model = new()
            {
                CountryName = state.Country.Name,
                Id = state.Id,
                Name = state.Name
            };

            return model;
        }

        public static EditStateVM GetEditViewModel(State state)
        {
            EditStateVM model = new()
            {
                CountryName = state.Country.Name,
                Id = state.Id,
                Name = state.Name
            };

            return model;
        }

        public static RestoreStateVM GetRestoreViewModel(State state)
        {
            RestoreStateVM model = new()
            {
                CountryName = state.Country.Name,
                Id = state.Id,
                Name = state.Name
            };

            return model;
        }

        public static List<SelectListItem> GetStatesAsSelectItems(List<State> states)
        {
            List<SelectListItem> selectItems = [];

            foreach (State state in states)
            {
                selectItems.Add(new SelectListItem()
                {
                    Value = state.Id,
                    Text = state.Name
                });
            }

            return selectItems;
        }

        private static List<StateDTO> MapModelToDTO(List<State> states)
        {
            List<StateDTO> dtos = new(states.Count);

            foreach (State state in states)
            {
                StateDTO dto = new(
                    state.Id,
                    state.Name,
                    state.Country.Name);

                dtos.Add(dto);
            }

            return dtos;
        }
    }
}

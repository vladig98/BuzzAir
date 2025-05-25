namespace BuzzAir.Factories
{
    public static class TimezoneFactory
    {
        public static List<SelectListItem> GetTimezonesForSelect(Timezone[] timezoneModels)
        {
            int count = timezoneModels.Length;
            string defaulGroupName = "Other";
            List<SelectListItem> timezones = new(count);

            Dictionary<string, SelectListGroup> groups = [];
            SelectListGroup defaultGroup = new()
            {
                Name = defaulGroupName
            };

            groups.Add(defaulGroupName, defaultGroup);

            for (int i = 0; i < count; i++)
            {
                Timezone timezone = timezoneModels[i];
                string? groupName = null;

                if (timezone.Name.Contains('/'))
                {
                    groupName = timezone.Name.Split('/')[0];
                }

                if (groupName != null && !groups.TryGetValue(groupName, out SelectListGroup? group))
                {
                    group = new SelectListGroup()
                    {
                        Name = groupName
                    };

                    groups.Add(groupName, group);
                }

                SelectListItem timezoneSelectItem = new()
                {
                    Text = timezone.Name,
                    Value = timezone.Id,
                    Group = groupName == null ? groups[defaulGroupName] : groups[groupName]
                };

                timezones.Add(timezoneSelectItem);
            }

            timezones = [.. timezones.OrderBy(x => x.Group.Name).ThenBy(x => x.Text)];

            return timezones;
        }
    }
}

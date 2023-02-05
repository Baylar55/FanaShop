function filterAccordion(section, heading, list) {
    $(section).each(function() {
      var that = this,
          listHeight = $(this).find(list).height();
  
      $(this).find(heading).click(function() {
        $(this).toggleClass("plus");
        $(that).find(list).toggle({
          "height": "0"
        }, 250);
      });
    });
  };
  
  filterAccordion('.filter-item', '.filter-item-inner-heading', '.filter-attribute-list');

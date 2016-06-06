using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Fotobel.Classes;
using Fotobel.Models;
using PhotoMSK.Data;
using PhotoMSK.Data.Models.Menu;
using PhotoMSK.ViewModels.Menu;
using PhotoMSK.ViewModels.Realisation;
using PhotoMSK.ViewModels.Route;

namespace Fotobel.Api.Version2
{

    public class MenuItemController : ApiController
    {
        readonly AppContext _context = new AppContext();
        [HttpGet]
        public List<AbstractMenuItemViewModel.Summary> GetAbstracrt(string id)
        {
            var menu = _context.AbstractMenuItem.Where(x => x.RouteEntity.Shortcut == id).ToList();
            return menu.As<List<AbstractMenuItemViewModel.Summary>>();
        }

        [HttpGet]
        public IHttpActionResult Get(string shortcut, Guid? routeMenuId = null)
        {
            if (!routeMenuId.HasValue)
                return
                    Ok(_context.RouteMenus.Where(x => x.RouteEntity.Shortcut == shortcut)
                        .ToList()
                        .As<List<MenuItemViewModel>>());

            var menu = _context.AbstractMenuItem.Where(x => x.RouteEntity.Shortcut == shortcut && x.ParentID == null).ToList();

            var item = _context.RouteMenus.Find(routeMenuId.Value);
            var viewmodel = LoadChildrenFor(item);

            var freemenus = menu.Where(x=> x is RouteMenu==false).As<List<MenuItemViewModel>>();

            return Ok(new
            {
                menu = viewmodel,
                freeMenu = freemenus
            });
        }

        private MenuItemViewModel LoadChildrenFor(RouteMenu item)
        {
            var newitem = item.As<MenuItemViewModel>();
            var ids = _context.AbstractMenuItem.Where(x => x.ParentID == item.ID).Select(x => x.ID).ToList();
            foreach (var children in LoadChildrenFor(ids))
            {
                newitem.Add(children);
            }

            return newitem;

        }

        private IList<MenuItemViewModel> LoadChildrenFor(List<Guid> ids)
        {

            var nodes = _context.AbstractMenuItem.Where(x => ids.Contains(x.ID) || (x.ParentID != null && ids.Contains(x.ParentID.Value))).ToList();

            Dictionary<Guid, MenuItemViewModel> dictionary = nodes.Where(x => ids.Contains(x.ID)).As<IList<MenuItemViewModel>>().ToDictionary(x => x.ID);

            var iids = nodes.Where(x => x.ParentID != null && ids.Contains(x.ParentID.Value)).Select(x => x.ID).ToList();

            if (iids.Any())
            {
                foreach (var children in LoadChildrenFor(iids))
                {
                    if (children.ParentID != null) dictionary[children.ParentID.Value].Add(children);
                }
            }

            return dictionary.Values.ToList();

        }


        [Authorize]
        [HttpPost]
        [Message("Меню создано")]
        public IHttpActionResult Post(string shortcut, MenuItemViewModel model)
        {
            AbstractMenuItem item = null;

            var rid = _context.Routes.Single(x => x.Shortcut == shortcut).ID;
            switch (model.Type)
            {
                case "PageMenuItem":
                    item = new PageMenuItem
                    {
                        ID = Guid.NewGuid(),
                        Name = model.Name,
                        Order = model.Order,
                        Publish = model.Publish,
                        RouteID = rid,
                        BasePage = _context.BasePages.Find(model.BasePage.ID),
                        TextPageID = model.BasePage.ID
                    };
                    ;
                    break;
                case "LinkMenuItem":
                    item = new LinkMenuItem
                    {
                        ID = Guid.NewGuid(),
                        Name = model.Name,
                        Order = model.Order,
                        Link = model.Link,
                        Publish = model.Publish,
                        RouteID = rid
                    };
                    break;
                case "RouteMenu":
                    item = new RouteMenu
                    {
                        ID = Guid.NewGuid(),
                        Name = model.Name,
                        Order = model.Order,
                        Publish = model.Publish,
                        RouteID = rid
                    };
                    break;
                default:
                    throw new NotImplementedException();
            }

            _context.AbstractMenuItem.Add(item);
            _context.SaveChanges();
            if (item is RouteMenu)
            {
                return Ok(item.As<RouteMenuViewModel.Details>());
            }
            if (item is LinkMenuItem)
            {
                return Ok(item.As<LinkMenuItemViewModel.Summary>());
            }
            return Ok(item.As<PageMenuItemViewModel.Summary>());
        }

        [Authorize]
        [HttpPut]
        [Message("Обновлено")]
        public IHttpActionResult Put(string shortcut, MenuItemViewModel models)
        {
            UpdateModel(models);

            _context.SaveChanges();
            return Ok();
        }

        private AbstractMenuItem UpdateModel(MenuItemViewModel model)
        {
            var item = _context.AbstractMenuItem.Find(model.ID);

            if (item == null)
                switch (model.Type)
                {
                    case "PageMenuItem":
                        item = new PageMenuItem
                        {
                            ID = Guid.NewGuid(),
                            Name = model.Name,
                            Order = model.Order,
                            Publish = model.Publish,
                            RouteID = model.RouteID,
                            BasePage = _context.BasePages.Find(model.BasePage.ID),
                            TextPageID = model.BasePage.ID
                        }; ;
                        break;
                    case "LinkMenuItem":
                        item = new LinkMenuItem
                        {
                            ID = Guid.NewGuid(),
                            Name = model.Name,
                            Order = model.Order,
                            Link = model.Link,
                            Publish = model.Publish,
                            RouteID = model.RouteID
                        };
                        break;
                    default:
                        throw new NotImplementedException();
                }

            foreach (var abstractMenuItem in item.Nodes)
            {
                abstractMenuItem.ParentID = null;
            }
            item.Nodes.Clear();
            foreach (var menuItemViewModel in model.Nodes)
            {
                var nitem = UpdateModel(menuItemViewModel);
                item.AddNode(nitem);
            }

            return item;
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(Guid id)
        {
            return Ok();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _context.Dispose();
            base.Dispose(disposing);
        }
    }
}

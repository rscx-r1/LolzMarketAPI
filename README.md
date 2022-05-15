# C# LolzMarketAPI
## Category [ID - Name] list
* `1`  `steam` - Steam
* `2`  `vkontakte` - VK
* `3`  `origin` - Origin
* `4`  `warface` - Warface
* `5`  `uplay` - Uplay
* `7`  `socialclub` - Social Club
* `9`  `fortnite` - Fortnite
* `10` `instagram` - Instagram
* `11` `battlenet` - Battle.net
* `12` `epic-games` - Epic Games
* `14` `world-of-tanks` - World Of Tanks
* `15` `supercell` - Supercell
* `16` `wot-blitz` - World Of Tanks Blitz
* `17` `genshin-impact` - Genshin Impact
* `18` `escape-from-tarkov` - Escape From Tarkov
* `19` `vpn` - VPN
* `20` `tiktok` - TikTok
* `22` `discord` - Discord

## First step to working with that API.
Firstly we need to make a reference for our API.
![image](https://user-images.githubusercontent.com/53280415/168472392-bd1ff28d-763e-4738-9dd2-6a017f21e519.png)
![image](https://user-images.githubusercontent.com/53280415/168472474-7fedaf98-7e13-4fc7-adf0-17c7b1e0a501.png)

Include a namespace for API.
```c#
using LolzMarketAPI;
```
After including you need to set an authorization data in your class initialization.
```c#
public Form1()
{
   LolzMarketAPI.setData("your_token", user_id);
   InitializeComponent();
}
```

This C# API has 4 methods for sending requests.
```GET | POST | PUT | DELETE```

All of the functions returns an string formatted JSON object.

# Accounts list/information
## List
```c#
response = LolzMarketAPI.List(**args);
```
#### arguments
`string category` | `Optional` // The category name to view specified items with that category

`int pmin` | `Optional` // Minimal price of account (Inclusive)

`int pmax` | `Optional` // Maximum price of account (Inclusive)

`string title` | `Optional` // The word or words contained in the account title

`bool parse_sticky_items` | `Optional` // If yes, API will return stickied accounts in results


> This function displays a list of latest accounts if category is not introduced or displays a list of accounts in a specific category according to your parameters with category introduced.

## Items
```c#
response = LolzMarketAPI.Items(**args);
```
#### arguments
`int category_id` | `Optional` // The category id to view specified items with that category

`int pmin` | `Optional` // Minimal price of account (Inclusive)

`int pmax` | `Optional` // Maximum price of account (Inclusive)

`string title` | `Optional` // The word or words contained in the account title

`bool parse_sticky_items` | `Optional` // If yes, API will return stickied accounts in results

> This function displays a list of owned accounts

## Orders
```c#
response = LolzMarketAPI.Orders(**args);
```
#### arguments
`int category_id` | `Optional` // The category id to view specified items with that category

`int pmin` | `Optional` // Minimal price of account (Inclusive)

`int pmax` | `Optional` // Maximum price of account (Inclusive)

`string title` | `Optional` // The word or words contained in the account title

`bool parse_sticky_items` | `Optional` // If yes, API will return stickied accounts in results

> This function displays a list of purchased accounts

## Fave
```c#
response = LolzMarketAPI.Fave();
```
> This function displays a list of favourites accounts

## Viewed
```c#
response = LolzMarketAPI.Viewed();
```
> This function displays a list of viewed accounts

## Item
```c#
response = LolzMarketAPI.Item(int itemId);
```
> This fucntion displays account information with that itemId

# Account purchasing
To purchase the account you need make 3 steps Reserve() | Check_Account() | Confirm_Buy()
## Reserve
```c#
response = LolzMarketAPI.Reserve(int itemId, int price);
```
> This function reserves account for you. Reserve time - 300 seconds.

## Cancel_Reserve
```c#
response = LolzMarketAPI.Cancel_Reserve(int itemId);
```
> This function cancels account reserve.

## Check_Account
```c#
response = LolzMarketAPI.Check_Account(int itemId);
```
> Checking account for validity. If the account is invalid, the purchase will be canceled automatically (you don't need to make request Cancel-Reserve)

## Confirm_Buy
```c#
response = LolzMarketAPI.Confirm_Buy(int itemId);
```
> This function confirms account buy.

# Money transfers and payments list
## Transfer_Money
```c#
response = LolzMarketAPI.Transfer_Money(**args);
```
#### arguments
`int receiverId` | `Required` User id of receiver. If user_id specified, username is not required.

`string receiverUsername` | `Required` Username of receiver. If username specified, user_id is not required.

`int amount` | `Required` Amount to send in your currency.

`string secret_answer` |  secret_answer `Required` Secret answer of your account

`string currency` | `Required` Using currency for amount. Allowed values: cny usd rub eur uah kzt byn gbp

`string comment` | `Optional` Transfer comment. Maximum 255 characters

`bool transfer_hold` | `Optional` Hold transfer or not.

`int hold_length_value` | `Optional` Hold length value (number).

`string hold_length_option` | `Optional` Hold length option (string). Allowed values: hour day week month year

## Hold parameters examples
E.g. you want to hold money transfer on 3 days. hold_length_value - will be '3', hold_length_option - will be 'days'.

E.g. you want to hold money transfer on 12 hours. hold_length_value - will be '12', hold_length_option - will be 'hours'
> This functions sends money to any user.

## Payments
```c#
response = LolzMarketAPI.Payments(int userId, **args);
```
#### arguments
`int userId` | Your user id to see information

`string type` | `Optional`: Type of operation. Allowed operation types: income cost refilled_balance withdrawal_balance paid_item sold_item money_transfer receiving_money internal_purchase claim_hold

`int pmin` | `Optional`: Minimal price of operation (Inclusive)

`int pmax` | `Optional`: Maximum price of operation (Inclusive)

`string receiver` | `Optional`: Username of user, which receive money from you

`string sender` | `Optional`: Username of user, which sent money to you

`string startDate` | `Optional`: Start date of operation (RFC 3339 date format)

`string endDate` | `Optional`: End date of operation (RFC 3339 date format)

`string wallet` | `Optional`: Wallet, which used for money payots

`string comment` | `Optional`: Comment for money transfers

`int is_hold` | `Optional`: Display hold operations

> This function displays list of your payments

# Account publishing
You need to make 2 requests: Add_Item() and Goods_Check()
For categories, which required temporary email (Steam, Social Club) you need to make Goods_Add() to get temporary email
## Item_Add
```c#
response = LolzMarketAPI.Add_Item(**args);
```
#### arguments
`string title` | `Required` Russian title of account. If title specified and title_en is empty, title_en will be automatically translated to English language.

`int price` | `Required` Account price in your currency

`int category_id` | `Required` Category id

`string item_origin` | `Required` Item origin

`int extended_guarantee` | `Required` Guarantee type. Allowed values: -1 = 12 hours, 0 = 24 hours, 1 = 3 days.

`string currency` | `Required` Using currency. Allowed values: cny usd rub eur uah kzt byn gbp

`string title_en` | `Optional` English title of account. If title_en specified and title is empty, title will be automatically translated to Russian language.

`string description` | `Optional` Account public description

`string information` | `Optional` Account private information (visible for buyer only if purchased)

`bool? has_email_login_data` | `Optional` / Required if a category is one of list of Required email login data categories (see below)

`string email_login_data` | `Optional` / Required_ if a category is one of list of Required email login data categories (see below) . Email login data (login:pass format)

`string email_type` | `Optional` Email type. Allowed values: native autoreg

`bool? allow_ask_discount` | `Optional` Allow users to ask discount for this account

`int proxy_id` | `Optional` Using proxy id for account checking. See Proxy Settings to get or edit proxy list

> This function adds account on the market. After this request an account will have item_state = awaiting (not displayed in search)
### Item origin
Account origin. Where did you get it from.

* `brute` - Account received using Bruteforce
* `fishing` - Account received from fishing page
* `stealer` - Account received from stealer logs
* `autoreg` - Account is automatically registered by a tool
* `personal` - Account is yours. You created it yourself.
* `resale` - Account received from another seller
* `retrive` - Account is recovered by email or phone (only for VKontakte category)

Required email login data categories
* `Fortnite` `(id 9)`
* `Epic games` `(id 12)`
* `Escape from Tarkov` `(id 18)`

## Goods_Add
```c#
response = LolzMarketAPI.Goods_Add(int itemId, **args);
```
#### arguments
`int itemId` | `Required` Item id to add

`int resell_item_id` | `Optional` Put item id, if you are trying to resell item. This is useful to pass temporary email from reselling item to new item. You will get same temporary email from reselling account.

> This function get info about not published item. For categories, which required temporary email (Steam, Social Club), you will get temporary email in response.

## Goods_Check
```c#
response = LolzMarketAPI.Goods_Check(int itemId, **args);
```
#### arguments
`int itemId` | `Required Item id to add

`string login` | `Optional` Account login (or email)

`string password` | `Optional` Account password

`string login_password` | `Optional` Account login data format login:password

`bool? close_item` | `Optional` If set, the item will be closed item_state = closed

`Dictionary<string, string> extra` | `Optional` (Array) Extra params for account checking. E.g. you need to put cookies to extra[cookies] if you want to upload Fortnite/Epic Games account {"title": "value", "title2": "value2"}

`int resell_item_id` | Put if you are trying to resell an account.

> This function check account on validity. If account is valid, account will be published on the market.


## Category
```c#
response = LolzMarketAPI.Category(**args);
```
#### arguments
`bool? top_queries` | `Optional` (Boolean) Display top queries for per category

> This function display category list

# Accounts managing
## Email_Code
```c#
response = LolzMarketAPI.Email_Code(string email);
```
#### arguments
`string email` | `Required` Account email

> This function gets confirmation code or link.

## Refuse_Guarantee
```c#
response = LolzMarketAPI.Refuse_Guarantee(int itemId);
```
#### arguments
`int itemId` | `Required` itemId for refusing

> This function cancel guarantee of account. It can be useful for account reselling.

## Change_Password
```c#
response = LolzMarketAPI.Change_Password(int itemId, **args);
```
#### arguments
`int itemId` | `Required` itemId to change password

`bool? _cancel` | `Optional` Cancel change password recommendation. It will be helpful, if you don't want to change password and get login data

> This function changes password of account.

## Edit_Item
```c#
response = LolzMarketAPI.Edit_Item(int itemId, **args);
```
#### arguments
`int itemId` | `Required` itemId to edit

`string key` | `Optional` Key to edit (key list you can see below). E.g. price.

`string value` | `Optional` Value to edit

`Dictionary<string, string> key_values` | `Optional` Key-values to edit (Array). E.g. {"title": "value", "title2": "value2"}

`string currency = "rub"` | `Required` Currency of account price. Required if you are trying to change price field. Allowed values: cny usd rub eur uah kzt byn gbp | Defaults to "rub"

### Key list
 * `title` `Optional` Russian title of account. If `title` specified and `title_en` is empty, `title_en` will be automatically translated to English language.
 * `title_en` `Optional` English title of account. If `title_en` specified and `title` is empty, `title` will be automatically translated to Russian language.
 * `price` `Optional` Account price in your currency. Allowed values: `cny` `usd` `rub` `eur` `uah` `kzt` `byn` `gbp`
 * `item_origin` `Optional` Item origin
 * `description` `Optional` Account public description
 * `information` `Optional` Account private information (visible for buyer only if purchased)
 * `has_email_login_data` `Optional` Set boolean, if you have email login data
 * `email_login_data` `Optional` Email login data (login:pass format) 
 * `email_type` `Optional` Email type. Allowed values: `native`, `autoreg`
 * `allow_ask_discount` `Optional` Allow users to ask discount for this account
 * `proxy_id` `Optional` Using proxy id for account checking. See [GET /account/market](#account/market) to get or edit proxy list
> This function edits any details of account.

## Delete_Item
```c#
response = LolzMarketAPI.Delete_Item(int itemId, string reason);
```
#### arguments
`int itemId` | `Required` itemId to delete

`string reason` | `Required` Delete reason

> This function deletes your account from public search. Deletetion type is soft. You can restore account after deletetion if you want.

## Add_Tag
```c#
response = LolzMarketAPI.Add_Tag(int itemId, int tagId);
```
#### arguments
`int itemId` | `Required` itemId to add tag

`int tagId` | `Required` Tag id (Tag list is available via Me())

> This function adds tag for the account

## Delete_Tag
```c#
response = LolzMarketAPI.Delete_Tag(int itemId, int tagId);
```
#### arguments
`int itemId` | `Required` itemId to delete tag

`int tagId` | `Required` Tag id (Tag list is available via Me())

> This function deletes tag for the account

## Bump
```c#
response = LolzMarketAPI.Bump(int itemId);
```
#### arguments
`int itemId` | `Required` itemId to bump

> This function bumps account in the search

## Star
```c#
response = LolzMarketAPI.Star(int itemId);
```
#### arguments
`int itemId` | `Required` itemId to star product

> This function adds account to favourites

## Unstar
```c#
response = LolzMarketAPI.Unstar(int itemId);
```
#### arguments
`int itemId` | `Required` itemId to unstar product

> This function removes account from favourites

## Stick
```c#
response = LolzMarketAPI.Stick(int itemId);
```
#### arguments
`int itemId` | `Required` itemId to stick product

> This function stick account in the top of search

## Unstick
```c#
response = LolzMarketAPI.Unstick(int itemId);
```
#### arguments
`int itemId` | `Required` itemId to unstick product

> This function unstick account of the top of search

## Change_Owner
```c#
response = LolzMarketAPI.Change_Owner(int itemId, string username, string secret_answer);
```

> This function changes account owner by username

# Market profile settings
## Me
```c#
response = LolzMarketAPI.Me();
```
> This function displays info about your profile. 

## Change_Settings
```c#
response = LolzMarketAPI.Change_Settings(**args);
```
#### arguments
`bool? disable_steam_guard` | `Optional` Disable Steam Guard on account purchase moment

`bool? user_allow_ask_discount` | `Optional` Allow users ask discount for your accounts

`uint? max_discount_percent` | `Optional` Maximum discount percents for your accounts

`string allow_accept_accounts` | `Optional` Usernames who can transfer market accounts to you. Separate values with a comma.

`bool? hide_favourites` | `Optional` Hide your profile info when you add an account to favorites

> This function change settings about your profile on the market

# Proxy settings
## Get_Proxy
```c#
response = LolzMarketAPI.Get_Proxy();
```
> This function gets your proxy list

## Add_Proxy
```c#
response = LolzMarketAPI.Add_Proxy(string proxyIp, int proxyPort, **args);
```
#### arguments
`string proxyIp` | `Required` Proxy ip or host

`int proxyPort` | `Required` Proxy port

`string proxyUser` | `Optional` Proxy username

`string proxyPass` | `Optional` Proxy password

_____
`string proxyRow` | `Required` Proxy list in String format ip:port:user:pass. Each proxy must be start with new line (use \r\n separator)

> This function add single proxy or proxy list

## Delete_Proxy
```c#
response = LolzMarketAPI.Delete_Proxy(**args);
```
#### arguments
`int proxy_id` | `Optional` Proxy id

`bool? delete_all` | `Optional` Set boolean if you want to delete all proxy

> This function delete single or all proxies

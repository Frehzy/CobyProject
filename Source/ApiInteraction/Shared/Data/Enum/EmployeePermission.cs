namespace Shared.Data.Enum;

public enum EmployeePermission : byte
{
    CanCreateOrder, //заказ
    CanUpdateOrder,
    CanCloseOrder,
    CanRemoveOrder,

    CanAddGuestOnOrder, //гость на заказе
    CanRemoveGuestOnOrder,

    CanAddPaymentOnOrder, //оплата на заказ
    CanRemovePaymentOnOrder,

    CanAddDishesOnOrder, //блюдо на заказе
    CanRemoveDishesOnOrder,
    CanRemovePrintedDishesOnOrder,

    CanOpenCaseSession, //кассовая смена
    CanCloseCafeSession,

    CanOpenOrdersOfOtherWaiters, //другие официанты
    CanClosePersonalShiftOfOtherWaiters,

    CanAcceptPayment, //закрытие заказа

    CanSeeCaseSessionReport, //отчёты по кассовой смене

    CanAddDiscountOnOrder, //скидка на заказе
    CanRemoveDiscountOnOrder,

    CanSeeClosedOrders, //просматривать закрытые заказы

    CanChangeTableOnOrder, //менять номер стола

    CanChangeWaiterOnOrder //менять официанта на столе
}